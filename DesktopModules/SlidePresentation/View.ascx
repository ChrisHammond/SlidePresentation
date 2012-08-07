<%@ Control Language="C#" Inherits="Christoc.Com.Modules.SlidePresentation.View" AutoEventWireup="false" CodeBehind="View.ascx.cs" %>
<div id="SlidePresentation">
    <!-- ko foreach: slides -->
    <div class="step" data-bind="attr:{'data-x':XVal,'data-y':YVal}">
        <div data-bind="html:Body">
        </div>
    </div>
    <!-- /ko -->
</div>


<script language="javascript" type="text/javascript">
    jQuery(document).ready(function ($) {
        var md = new SlidePresentation($, ko, {
            moduleId:<% = ModuleId %>,
            tabId: <% = TabId %>,
            serverErrorText: '<%=DotNetNuke.UI.Utilities.ClientAPI.GetSafeJSString(LocalizeString("ServerError"))%>',
            servicesFramework: $.ServicesFramework(<%=ModuleContext.ModuleId %>)   
        });
        md.init('#SlidePresentation');
    });


</script>
