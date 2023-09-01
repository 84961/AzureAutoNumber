// See https://aka.ms/new-console-template for more information
using AutoNumber.Interfaces;
using AutoNumber;
using Azure.Storage.Blobs;
//var store = new DebugOnlyFileDataStore("C:\\Codes\\AzureAutoNumber\\testblob");
string connstring = "DefaultEndpointsProtocol=https;AccountName=erissandboxdeveastus;AccountKey=IADmZ4qRk7/GwouuV3Az+0AyiMIrGtdxzDGJu/WMrakXrdQe+QRMR64/E0ueyB0rhpqE0ciKzqgMBNyn4rzqDQ==;EndpointSuffix=core.windows.net";
string container = "poc";
var blobClient =  new BlobServiceClient(connstring);

var store = new BlobOptimisticDataStore(blobClient, container);
store.GetData("test");
store.TryOptimisticWrite("test", "1000");
var subject = new UniqueIdGenerator(store)
{
    BatchSize = 10
};

List<Task> tasks = new List<Task>();
int i = 10;
while(i > 0)
{
    tasks.Add(Task.Run(() => Console.WriteLine(subject.NextId("test"))));
    i--;
}


await Task.WhenAll(tasks);