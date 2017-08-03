
function ChkMinusDbl(ctl, e) {
    //var evt = e ? e : window.event; 
    var zz = e.keyCode || e.charCode;
    if (zz == 8 || zz == 9 || zz == 37 || zz == 39)
        return;


    if (zz < 48 || zz > 57) {
        if (e.charCode == 46) {  //e.charCode == 46 = .
            if (ctl.value.indexOf(".", 0) >= 0) {
                if (window.event) {//IE 
                    var ieVersion = parseFloat(navigator.appVersion);
                    if (ieVersion == 5)  //IE 9, 10 
                        e.preventDefault();
                    else if (ieVersion == 4)  //IE 7,8 
                        event.returnValue = false;
                } else if (e) {//Firefox 
                    if (e.keyCode == 46) //e.keyCode = 46 = Del
                        return;

                    e.preventDefault();
                }
            }
        }
        else {
            if (window.event) {//IE 
                var ieVersion = parseFloat(navigator.appVersion);
                if (ieVersion == 5)  //IE 9, 10 
                    e.preventDefault();
                else if (ieVersion == 4)  //IE 7,8 
                    event.returnValue = false;
            } else if (e) {//Firefox 
                if (e.keyCode == 46) //e.keyCode = 46 = Del
                    return;

                e.preventDefault();
            }
        }
    }
}


function ChkMinusInt(ctl, e) {
    //var evt = e ? e : window.event; 
    var zz = e.keyCode || e.charCode;
    if (zz == 8 || zz == 9 || zz == 37 || zz == 39)
        return;

    if (zz < 48 || zz > 57) {
        if (window.event) {//IE 
            var ieVersion = parseFloat(navigator.appVersion);
            if (ieVersion == 5)  //IE 9, 10 
                e.preventDefault();
            else if (ieVersion == 4)  //IE 7,8 
                event.returnValue = false;
        } else if (e) {//Firefox 
            if (e.keyCode == 46) //e.keyCode = 46 = Del
                return;

            e.preventDefault();
        }
    }
}


function CheckKeyNumber(e) {
    //삯系⊥묀櫓〈쀼窪  ctrl 笑剋陌졔饑 V 
    var evt = e ? e : window.event;
    var keyCode = evt.keyCode || evt.charCode;

    if ((keyCode == 17) || (keyCode == 86)) {
        if (window.event) {//IE 
            var ieVersion = parseFloat(navigator.appVersion);
            //alert(ieVersion); 
            if (ieVersion == 5)  //IE 9, 10 
                e.preventDefault();
            else if (ieVersion == 4)  //IE 7,8 
                event.returnValue = false;
        } else if (e) {//Firefox 
            if (e.keyCode == 46) //e.keyCode = 46 = Del
                return;

            e.preventDefault();
        }
    }
}


function checkTextAreaMaxLength(textBox, e, length) {
    var mLen = textBox["MaxLength"];
    if (null == mLen)
        mLen = length;

    var maxLength = parseInt(mLen);
    if (!checkSpecialKeys(e)) {
        if (textBox.value.length > maxLength - 1) {
            if (window.event) {//IE 
                var ieVersion = parseFloat(navigator.appVersion);
                if (ieVersion == 5)  //IE 9, 10 
                    e.preventDefault();
                else if (ieVersion == 4)  //IE 7,8 
                    event.returnValue = false;
            } else if (e) {//Firefox 
                if (e.keyCode == 46) //e.keyCode = 46 = Del
                    return;

                e.preventDefault();
            }
        }
    }
}

function ChkNonKeySpecialChar(ctl, e) {
    var zz = e.keyCode || e.charCode;
    //alert(e.keyCode + 'AAA' + e.charCode + ' aaa ' + zz);
    //return;

    if (zz == 8 || zz == 9 || zz == 32)
        return;

    if ((zz >= 32 && zz <= 47) || (zz >= 58 && zz <= 64) || (zz >= 91 && zz <= 96) || (zz >= 123 && zz <= 126)) {
        if (window.event) {//IE 
            var ieVersion = parseFloat(navigator.appVersion);
            if (ieVersion == 5)  //IE 9, 10 
                e.preventDefault();
            else if (ieVersion == 4)  //IE 7,8 
                event.returnValue = false;
        } else if (e) {//Firefox 
            if (e.keyCode == 46) //e.keyCode = 46 = Del
                return;

            e.preventDefault();
        }
    }
}

function CheckCtrlV(e) {
    //삯系⊥묀櫓〈쀼窪  ctrl 笑剋陌졔饑 V 
    var evt = e ? e : window.event;
    var keyCode = evt.keyCode || evt.charCode;

    alert(e.keyCode + '$$$$' + keyCode + '####' + evt.charCode);

    if (keyCode == 17) {
        if (window.event) {//IE 
            var ieVersion = parseFloat(navigator.appVersion);
            //alert(ieVersion); 
            if (ieVersion == 5)  //IE 9, 10 
                e.preventDefault();
            else if (ieVersion == 4)  //IE 7,8 
                event.returnValue = false;
        } else if (e) {//Firefox 
            //alert("firefox");
            e.preventDefault();
        }
    }
}

function AddComma(txtId) {
    var txt = document.getElementById(txtId);
    var valIn = txt.value;

    if (parseFloat(valIn) < 0.00)
        j = 4;
    else
        j = 3;

    //var i = posStart;
    var i = formatDbl(valIn).length - 3;
    //i = 0;              this line for block addcomma to not working...
    var temp = formatDbl(valIn);
    while (i > j) {
        i = i - 3;
        //ret = temp.toString();
        temp = temp.substr(0, i) + "," + temp.substr(i, temp.length);

    }

    txt.value = temp;
}

function ClearComma(txtId) {
    var txt = document.getElementById(txtId);
    var temp = txt.value;
    while (temp.indexOf(",", 0) != -1)
        temp = temp.replace(",", "");

    txt.value = temp;
    txt.select();
}

function formatDbl(valIn) {

    var temp = valIn;
    if (isNaN(parseFloat(temp))) {
        temp = 0;
    }

    temp = "" + Math.round(parseFloat(temp) * 100);
    if (temp == 0)
        return '';
    else {
        if (parseFloat(temp) < 0) {
            temp = temp.substring(1, temp.length);
            var i = temp.length;
            while (i < 3) {
                temp = "0" + temp;
                i = i + 1;
            }
            i = i - 2;
            temp = "-" + temp.substring(0, i) + "." + temp.substring(i, temp.length);
        }
        else {
            var i = temp.length;
            while (i < 3) {
                temp = "0" + temp;
                i = i + 1;
            }
            i = i - 2;
            temp = temp.substring(0, i) + "." + temp.substring(i, temp.length);
        }
        return temp;
    }
}



function AddCommaInt(txtId) {
    var txt = document.getElementById(txtId);
    var valIn = txt.value;

    if (parseFloat(valIn) < 0.00)
        j = 4;
    else
        j = 3;

    //var i = posStart;
    var i = formatInt(valIn).length - 3;
    //i = 0;              this line for block addcomma to not working...
    var temp = formatInt(valIn);
    while (i > j) {
        i = i - 3;
        //ret = temp.toString();
        temp = temp.substr(0, i) + "," + temp.substr(i, temp.length);

    }

    txt.value = temp;
}
function formatInt(valIn) {

    var temp = valIn;
    if (isNaN(parseFloat(temp))) {
        temp = 0;
    }

    temp = "" + Math.round(parseFloat(temp) * 100);
    if (temp == 0)
        return '';
    else {
        if (parseFloat(temp) < 0) {
            temp = temp.substring(1, temp.length);
            var i = temp.length;
            while (i < 3) {
                temp = "0" + temp;
                i = i + 1;
            }
            i = i - 2;
            temp = "-" + temp.substring(0, i) //+ "." + temp.substring(i, temp.length);
        }
        else {
            var i = temp.length;
            while (i < 3) {
                temp = "0" + temp;
                i = i + 1;
            }
            i = i - 2;
            temp = temp.substring(0, i) //+ "." + temp.substring(i, temp.length);
        }
        return temp;
    }
}