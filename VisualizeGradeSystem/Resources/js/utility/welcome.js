﻿jQuery(function () {
    jQuery.get(
    "/Login/SavedeviceUniqueCode",
    {
        "deviceCode": getBrowserInfo()
    },
    function (data) {
        showTime();
    }
);
})

//设定倒数时间（秒）
var t = 3;

//显示倒数秒数  
function showTime() {
    jQuery("#info").text("将在" + t + "秒后进入系统");
    if (t == 1) {
        clearTimeout(showTime);
        location.href = '/User/Home';
    }
    if (t < 0)
    {
        jQuery("#info").text("进入中...请稍等");
    }
    t -= 1;
    setTimeout(showTime, 1000);
}