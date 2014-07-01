JSON-Sharp
==========

Libraries and utilities in C# to interact with JSON

### Extensions

This class contain two simple extensions to parse Datatables into a json object array or a javascript array.

```csharp
  
  DataTable dt = new DataTable(); 
  
  //... fill your datatable here ...
  
  SqlCommand comm = new SqlCommand("select firstName, lastName, id, type from some_table", _conn);
  comm.CommandType = CommandType.StoredProcedure;
  SqlDataAdapter oDataAdapter = new SqlDataAdapter(comm);
  oDataAdapter.Fill(oDataTable);
  oDataAdapter.Dispose();
  comm.Dispose();
  
  string json = dt.ToJson();
  
  string jarray = dt.ToJavaScriptArray();
  
``` 

The expected result:

```js

json = [
        {firstName : "john", lastName : "smith", id : 12, type: null },
        {firstName : "value", lastName : "value", id : 13, type: "book" },
        {firstName : "stuart", lastName : "lopez", id : 14, type: "movie" }
      ]
        
jarray = [
          ["john", "smith", 12, null],
          ["paul", "simons", 13, "book"]
          ["stuart", "lopez", 14, "movie"]
        ]
        
```
