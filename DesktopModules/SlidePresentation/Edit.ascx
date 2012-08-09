<%@ Control language="C#" Inherits="Christoc.Com.Modules.SlidePresentation.Edit" AutoEventWireup="false"  Codebehind="Edit.ascx.cs" %>
<%@ Register TagName="label" TagPrefix="dnn" Src="~/controls/labelcontrol.ascx" %>


<div class="dnnForm SlidePresentationDiv dnnClear" id="SlidePresentationDiv">

	<h2 id="dnnSitePanel-BasicSettings" class="dnnFormSectionHead"><a href="" class="dnnSectionExpanded"><%=LocalizeString("SlideCreation")%></a></h2>
	<fieldset>
        <div class="dnnFormItem">
            <dnn:Label ID="lblSlideBody" runat="server" /> 
 
            <asp:TextBox ID="txtSlideBody" runat="server" TextMode="MultiLine" Columns="50" Rows="15" />
        </div>
    </fieldset>

<asp:LinkButton runat="server" ID="lbSubmit" resourcekey="lbSubmit" CssClass="dnnPrimaryFormAction" OnClick="lbSubmit_Click"></asp:LinkButton>

</div>
<script language="javascript" type="text/javascript">
    /*globals jQuery, window, Sys */
    (function ($, Sys) {
        function SlidePresentationDiv() {
            $('#SlidePresentationDiv').dnnPanels();
        }

        $(document).ready(function () {
            SlidePresentationDiv();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
                SlidePresentationDiv();
            });
        });

    }(jQuery, window.Sys));
</script>