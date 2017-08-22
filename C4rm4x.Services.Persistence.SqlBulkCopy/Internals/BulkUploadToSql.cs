#region Using

using C4rm4x.Tools.Utilities;
using System.Collections.Generic;
using System.Data;
using System.Linq;

#endregion

namespace C4rm4x.Services.Persistence.SqlBulkCopy.Internals
{
    internal class BulkUploadToSql<TEntity>
            where TEntity : class
    {
        public int BatchSize { get; private set; }

        public string TableName { get; private set; }

        public DataTableMapping Mappings { get; private set; }

        private int RecordCount;

        private List<TEntity> InternalStore;

        public BulkUploadToSql(
            string tableName,
            int batchSize = 100)
        {
            tableName.NotNullOrEmpty(nameof(tableName));
            batchSize.Must(x => x > 0, "batchSize must be greater than 0");

            RecordCount = 0;
            InternalStore = new List<TEntity>();

            TableName = tableName;
            BatchSize = batchSize;
        }

        public void Initialise(DataTableMapping mappings)
        {
            mappings.NotNull(nameof(mappings));

            Mappings = mappings;
        }

        public void AddRange(IEnumerable<TEntity> itemsToAppend)
        {
            itemsToAppend.NotNullOrEmpty(nameof(itemsToAppend));

            InternalStore.AddRange(itemsToAppend);
        }

        public bool NextBatch(out DataTable dataTable)
        {
            dataTable = new DataTable(TableName);

            Setup(dataTable);
            PopulateBatch(dataTable);

            return dataTable.Rows.Count > 0;
        }

        private void Setup(DataTable dataTable)
        {
            Mappings.ForEach(mapping =>
                dataTable.Columns.Add(mapping.ColumnName, mapping.ColumnType));
        }

        private void PopulateBatch(DataTable dataTable)
        {
            var data = InternalStore.Skip(RecordCount).Take(BatchSize);

            foreach (var item in data)
            {
                var dataRow = dataTable.NewRow();

                PopulateDataRow(dataRow, item);
                dataTable.Rows.Add(dataRow);

                RecordCount++;
            }
        }

        private void PopulateDataRow(
            DataRow dataRow,
            TEntity item)
        {
            var properties = typeof(TEntity).GetProperties();

            Mappings.ForEach(mapping =>
            {
                var thisProperty = properties.FirstOrDefault(p => p.Name == mapping.PropertyName);

                dataRow[mapping.ColumnName] = thisProperty.GetValue(item, null);
            });
        }

        public void Clear()
        {
            RecordCount = 0;
            InternalStore.Clear();
        }
    }
}
