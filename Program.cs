﻿using ETLproj.Services;

string csvFilePath = "sample-cab-data.csv";
var records = ETLDataService.ExtractFromCsv(csvFilePath);
var distinctRecords = ETLDataService.RemoveDuplicates(records);