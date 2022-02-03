using Allowed.Ethereum.Common.Helpers;
using Allowed.Ethereum.Common.Models;
using Newtonsoft.Json;

string source = @"c:/Users/VodemSharp/Desktop/Projects/Work/DARS/DARS.Contracts/Source/";
string contractJson = File.ReadAllText(@$"{source}/bin/BEP20USDT.json");
StandardInput usdtInput = StandardInputHelper.GetStandardInput(source, contractJson);
File.WriteAllText("BEP20USDT-input.json", JsonConvert.SerializeObject(usdtInput, Formatting.Indented));