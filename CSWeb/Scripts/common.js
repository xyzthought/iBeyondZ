function SetDefaultButton(e) {
    var key;
    if (window.event)
        key = window.event.keyCode;     //IE
    else
        key = e.which;

    if (key == 13) {
        window.location.href = document.getElementById('lnkBtnSearch').href;

        return false;
    }
    else {
        return true;
    }



}



// version: beta
// created: 2005-08-30
// updated: 2005-08-31
// www.mredkj.com/tutorials/validate2.html for more detail
function extractNumber(obj, decimalPlaces, allowNegative) {
    var temp = obj.value;

    // avoid changing things if already formatted correctly
    var reg0Str = '[0-9]*';
    if (decimalPlaces > 0) {
        reg0Str += '\\.?[0-9]{0,' + decimalPlaces + '}';
    } else if (decimalPlaces < 0) {
        reg0Str += '\\.?[0-9]*';
    }
    reg0Str = allowNegative ? '^-?' + reg0Str : '^' + reg0Str;
    reg0Str = reg0Str + '$';
    var reg0 = new RegExp(reg0Str);
    if (reg0.test(temp)) return true;

    // first replace all non numbers
    var reg1Str = '[^0-9' + (decimalPlaces != 0 ? '.' : '') + (allowNegative ? '-' : '') + ']';
    var reg1 = new RegExp(reg1Str, 'g');
    temp = temp.replace(reg1, '');

    if (allowNegative) {
        // replace extra negative
        var hasNegative = temp.length > 0 && temp.charAt(0) == '-';
        var reg2 = /-/g;
        temp = temp.replace(reg2, '');
        if (hasNegative) temp = '-' + temp;
    }

    if (decimalPlaces != 0) {
        var reg3 = /\./g;
        var reg3Array = reg3.exec(temp);
        if (reg3Array != null) {
            // keep only first occurrence of .
            //  and the number of places specified by decimalPlaces or the entire string if decimalPlaces < 0
            var reg3Right = temp.substring(reg3Array.index + reg3Array[0].length);
            reg3Right = reg3Right.replace(reg3, '');
            reg3Right = decimalPlaces > 0 ? reg3Right.substring(0, decimalPlaces) : reg3Right;
            temp = temp.substring(0, reg3Array.index) + '.' + reg3Right;
        }
    }

    obj.value = temp;
}
function blockNonNumbers(obj, e, allowDecimal, allowNegative) {
    var key;
    var isCtrl = false;
    var keychar;
    var reg;

    if (window.event) {
        key = e.keyCode;
        isCtrl = window.event.ctrlKey
    }
    else if (e.which) {
        key = e.which;
        isCtrl = e.ctrlKey;
    }

    if (isNaN(key)) return true;

    keychar = String.fromCharCode(key);

    // check for backspace or delete, or if Ctrl was pressed
    if (key == 8 || isCtrl) {
        return true;
    }

    reg = /\d/;
    var isFirstN = allowNegative ? keychar == '-' && obj.value.indexOf('-') == -1 : false;
    var isFirstD = allowDecimal ? keychar == '.' && obj.value.indexOf('.') == -1 : false;

    return isFirstN || isFirstD || reg.test(keychar);
}


//****************************** END of Numeric validation


/**
* DHTML date validation script. Courtesy of SmartWebby.com (http://www.smartwebby.com/dhtml/datevalidation.asp)
*/
// Declaring valid date character, minimum year and maximum year
var dtCh = "/";
var minYear = 1900;
var maxYear = 2100;

function isInteger(s) {
    var i;
    for (i = 0; i < s.length; i++) {
        // Check that current character is number.
        var c = s.charAt(i);
        if (((c < "0") || (c > "9"))) return false;
    }
    // All characters are numbers.
    return true;
}

function stripCharsInBag(s, bag) {
    var i;
    var returnString = "";
    // Search through string's characters one by one.
    // If character is not in bag, append to returnString.
    for (i = 0; i < s.length; i++) {
        var c = s.charAt(i);
        if (bag.indexOf(c) == -1) returnString += c;
    }
    return returnString;
}

function daysInFebruary(year) {
    // February has 29 days in any year evenly divisible by four,
    // EXCEPT for centurial years which are not also divisible by 400.
    return (((year % 4 == 0) && ((!(year % 100 == 0)) || (year % 400 == 0))) ? 29 : 28);
}
function DaysArray(n) {
    for (var i = 1; i <= n; i++) {
        this[i] = 31
        if (i == 4 || i == 6 || i == 9 || i == 11) { this[i] = 30 }
        if (i == 2) { this[i] = 29 }
    }
    return this
}

function isDate(dtStr) {
    var daysInMonth = DaysArray(12)
    var pos1 = dtStr.indexOf(dtCh)
    var pos2 = dtStr.indexOf(dtCh, pos1 + 1)
    var strMonth = dtStr.substring(0, pos1)
    var strDay = dtStr.substring(pos1 + 1, pos2)
    var strYear = dtStr.substring(pos2 + 1)
    strYr = strYear
    if (strDay.charAt(0) == "0" && strDay.length > 1) strDay = strDay.substring(1)
    if (strMonth.charAt(0) == "0" && strMonth.length > 1) strMonth = strMonth.substring(1)
    for (var i = 1; i <= 3; i++) {
        if (strYr.charAt(0) == "0" && strYr.length > 1) strYr = strYr.substring(1)
    }
    month = parseInt(strMonth)
    day = parseInt(strDay)
    year = parseInt(strYr)
    if (pos1 == -1 || pos2 == -1) {
        alert("The date format should be : mm/dd/yyyy")
        return false
    }
    if (strMonth.length < 1 || month < 1 || month > 12) {
        alert("Please enter a valid month")
        return false
    }
    if (strDay.length < 1 || day < 1 || day > 31 || (month == 2 && day > daysInFebruary(year)) || day > daysInMonth[month]) {
        alert("Please enter a valid day")
        return false
    }
    if (strYear.length != 4 || year == 0 || year < minYear || year > maxYear) {
        alert("Please enter a valid 4 digit year between " + minYear + " and " + maxYear)
        return false
    }
    if (dtStr.indexOf(dtCh, pos2 + 1) != -1 || isInteger(stripCharsInBag(dtStr, dtCh)) == false) {
        alert("Please enter a valid date")
        return false
    }
    return true
}

function ValidateForm(obj) {

    if (isDate(obj.value) == false) {
        dt.focus()
        return false
    }
    return true
}


// Eliminate style from header menu
function resetAllMenuStyle() {
    for (var i = 0; i < 16; i++) {
        try {
            document.getElementById("mnuli" + (i + 1)).className = "";
        } catch (e) {

        }
    }
}


function trimString(s) {
    return rtrim(ltrim(s));
}

function ltrim(s) {
    var l = 0;
    while (l < s.length && s[l] == ' ')
    { l++; }
    return s.substring(l, s.length);
}

function rtrim(s) {
    var r = s.length - 1;
    while (r > 0 && s[r] == ' ')
    { r -= 1; }
    return s.substring(0, r + 1);
}

function ShowModalDiv(MainModalID, InnerDivID,TopOffset, iFrameID, iFrameheight, LeftPosition) {

    SetModalPosition(MainModalID, TopOffset, iFrameID, iFrameID, iFrameheight, LeftPosition);
    $('#' + MainModalID).show();
    if (null != InnerDivID) {
        $('#' + dvInnerWindow).show();
    }
    return false;
}


// Show modal window top and left position
function SetModalPosition(DivID, TopOffset, iFrameID, iFrameheight, LeftPosition) {
    //alert(DivID + '\n' + mouseTop);
    var dvPosition = 0;
    var scrolledTop = mouseTop - TopOffset;
    var browserName = navigator.appName;
    var winHeight = $(window).height() * 0.75;
    winHeight = Math.round(winHeight);
    $('#' + DivID).height(winHeight);

    var innerHeight = winHeight * 0.85;
    innerHeight = Math.round(innerHeight);
    if (null != iFrameID) {
        if (null != iFrameheight) {
            $('#' + iFrameID).height(iFrameheight);
        }
        else {
            $('#' + iFrameID).height(innerHeight);
        }
    }

    if (iFrameID == 'dvModalData2') {
        dvPosition = $(window).height() / 2 - winHeight / 2;
    }
    else {
        dvPosition = $(window).height() / 2 - $('#' + DivID).height() / 2;
    }


    if (docHeight < 3000) {
        scrolledTop = dvPosition;
    }

    var dvNewPosition = scrolledTop + 'px';
    dvPosition = Math.round(dvPosition);
    if (iFrameID == 'fff' || iFrameID == 'dvModalData2')
        var dvPositionWidth = $(window).width() / 2 - 522 / 2;
    else
        var dvPositionWidth = $(window).width() / 2 - $('#' + DivID).width() / 2;

    dvPositionWidth = Math.round(dvPositionWidth);

    //------------------------------ need to refine this code
    if (TopOffset < 0) {
        dvPositionWidth = '50%';
    }

    if (null != LeftPosition) {
        dvPositionWidth = LeftPosition;
    }
    //------------------------------


    $('#' + DivID).css({
        "position": "absolute",
        "top": dvNewPosition,
        "left": dvPositionWidth
    });

}




var docHeight = 0;
var mouseTop = 0;
jQuery(document).ready(function () {
    docHeight = $(document).height();
    $(document).mousemove(function (e) {
        mouseTop = e.pageY;
    });
})