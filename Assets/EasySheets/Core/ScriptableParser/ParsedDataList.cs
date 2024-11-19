using System.Collections.Generic;
using Google.Apis.Sheets.v4.Data;

namespace EasySheets.Core
{
    /// <summary>
    /// Represents a list of parsed data, providing methods to manage and access parsed data entries.
    /// </summary>
    public class ParsedDataList
    {
        /// <summary>
        /// The list of parsed data entries.
        /// </summary>
        protected List<ParsedData> _data { get; private set; }
        
        /// <summary>
        /// Gets the count of parsed data entries in the list.
        /// </summary>
        public int count => _data.Count;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ParsedDataList"/> class by converting a list of <see cref="ValueRange"/> objects.
        /// </summary>
        /// <param name="data">The list of <see cref="ValueRange"/> objects to be converted into parsed data.</param>
        public ParsedDataList(List<ValueRange> data)
        {
            _data = data.ConvertAll(d => new ParsedData(d));
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ParsedDataList"/> class with an existing list of <see cref="ParsedData"/> objects.
        /// </summary>
        /// <param name="data">The list of <see cref="ParsedData"/> objects to initialize the parsed data list.</param>
        public ParsedDataList(List<ParsedData> data)
        {
            _data = data;
        }
        
        /// <summary>
        /// Tries to get a <see cref="ParsedData"/> object at the specified index.
        /// </summary>
        /// <param name="index">The index of the parsed data to retrieve.</param>
        /// <param name="parsedData">The retrieved <see cref="ParsedData"/> object if the index is valid; otherwise, null.</param>
        /// <returns>True if the parsed data was retrieved successfully; otherwise, false.</returns>
        public bool TryGetParsedData(int index, out ParsedData parsedData)
        {
            if (index < 0 || index >= _data.Count)
            {
                parsedData = null;
                return false;
            }
            parsedData = _data[index];
            return true;
        }
    }
}
