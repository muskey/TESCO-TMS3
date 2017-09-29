var API = new playerAPI12();
function playerAPI12() {
	this.version = '1.2';
	this.increment = '';
	this.error = '0';
	this.initialized = false;
	this.keepValues = {};

	this.LMSInitialize = function() {
            console.log('LMSInitialize');
            return true;
        };
        
	this.LMSFinish = function() {
            console.log('LMSFinish');
            return true;
        };
	this.LMSGetValue = function(varName) {
            console.log('LMSGetValue => '+varName);
            return true;
        };
	this.LMSSetValue = function(varName, varValue) {
            console.log('LMSGetValue => '+varName+' = '+varValue);
            return true;
        };
	this.LMSCommit = function() {
            console.log('LMSCommit');
            return true;
        };
	this.LMSGetLastError = function() {
            console.log('LMSGetLastError');
            return true;
        };
	this.LMSGetErrorString = function() {
            console.log('LMSGetErrorString');
            return true;
        };
	this.LMSGetDiagnostic = function() {
            console.log('LMSGetDiagnostic');
            return true;
        };
}


var API_1484_11 = new playerAPI2004();
function playerAPI2004() {
	this.version = '2004';
	this.increment = '';
	this.error = '0';
	this.initialized = false;
	this.keepValues = {};

	this.Initialize = function() {
            console.log('Initialize');
            return true;
        };
        
	this.Terminate = function() {
            console.log('Terminate');
            return true;
        };
	this.GetValue = function(varName) {
            console.log('GetValue => '+varName);
            return true;
        };
	this.SetValue = function(varName, varValue) {
            console.log('SetValue => '+varName+' = '+varValue);
            return true;
        };
	this.Commit = function() {
            console.log('Commit');
            return true;
        };
	this.GetLastError = function() {
            console.log('GetLastError');
            return true;
        };
	this.GetErrorString = function() {
            console.log('GetErrorString');
            return true;
        };
	this.GetDiagnostic = function() {
            console.log('GetDiagnostic');
            return true;
        };
}
