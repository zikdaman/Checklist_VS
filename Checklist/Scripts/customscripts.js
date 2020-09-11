$(document).ready(function () {

    bindAttributeTypesDropdown();

    //$('#attributeTypesDropdown').change(function () {
    //    console.log('enter attribute dropdown');
    //    var el = $(this);
    //    var attributeType = el.val();
    //    $.ajax({
    //        data: { attributeType: attributeType },
    //        url: '/Templates/BlankTemplateAttribute',
    //        cache: false,
    //        success: function (html) {
    //            $("#attributesDiv").append(html);
    //            el.val('');
    //            setAttributeOrder();
    //        }
    //    });
    //    return false;
    //});
   
});

function bindAttributeTypesDropdown() {
    $('#attributeTypesDropdown').change(function () {
        console.log('enter attribute dropdown');
        var el = $(this);
        var attributeType = el.val();
        $.ajax({
            data: { attributeType: attributeType },
            url: '/Templates/BlankTemplateAttribute',
            cache: false,
            success: function (html) {
                $("#attributesDiv").append(html);
                el.val('');
                setAttributeOrder();
                bindCollapsible();
                bindAddToListBtn();
            }
        });
        return false;
    });
}

function bindCollapsible() {
    console.log('listCollapse');
    $('.listCollapse').click(function () {
        console.log('collapse');
        var thisCollapsibleDiv = $(this).parent().parent().next('.collapse');
        thisCollapsibleDiv.collapse('toggle');

    });
}

function setAttributeOrder() {
    $("input[name$='TemplateAttributeOrder']").each(function (i) {
        $(this).val(i);
    });
}



