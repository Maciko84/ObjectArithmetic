using Maciko84.ObjectArithmetic;
using System.Text.Json;
using System.Text.Json.Serialization.Metadata;
// Create a sample list of expressions
var expressions = new Operation[] {
    new("3 + 3"),
    new("9 / 3"),
    new(3, OperationMode.Modulo, 5)
};

// Encode to json
string json = JsonSerializer.Serialize<Operation[]>(expressions);

// Write To console
Console.WriteLine(json);