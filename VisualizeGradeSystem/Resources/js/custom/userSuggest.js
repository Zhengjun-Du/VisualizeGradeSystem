//用户意见反馈
function submitSuggest() {
    var suggestText = jQuery("#suggestText");
    var phone = jQuery("#phone");
    if (suggestText.val().length <= 0) {
        jQuery("#info").text("反馈内容不能为空！");
        return;
    }
    if (phone.val().length <= 0)
    {
        jQuery("#info").text("手机号不能为空！");
        return;
    }
    jQuery.post(
        "/User/Suggest",
        {
            "suggestText": suggestText.val(),
            "phone": phone.val(),
            "UserBrowser": getBroserInfo()
        },
        function (data) {
            if (data == "success") {
                suggestText.val("");
                jQuery("#info").text("反馈成功！");
            } else {
                jQuery("#info").text("反馈失败！");
            }
        },
            "text"
    );
}

//获得焦点清除提示信息
function clearInfo() {
    jQuery("#info").text("");
}