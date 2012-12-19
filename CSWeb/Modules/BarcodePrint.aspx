<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BarcodePrint.aspx.cs" Inherits="Modules_BarcodePrint" %>

<%@ Register Src="../UserControls/Header.ascx" TagName="Header" TagPrefix="uc1" %>
<%@ Register Src="../UserControls/Footer.ascx" TagName="Footer" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style type="text/css">
        @media print
        {
            body *
            {
                visibility: hidden;
            }
            .grid_container *
            {
                visibility: visible;
            }
            .grid_container
            {
                position: absolute;
                top: 40px;
                left: 30px;
            }
            
            .barcodeTarget{overflow:hidden!important;width:840px!Important;}
        }
    </style>
    <script type="text/javascript">
        function PrepareForPrint(printpage) {
            var divToPrint = document.getElementById(printpage);
            var popupWin = window.open('', '_blank', 'width=1100,height=900');
            var MediaPrint = "@page{margin:0} @media print { body { width: 1100 height: 900}  }";
            var vStyle = "<head><style type='text/css'>html,body {   margin:-7px;   padding:0;height:auto;} "+MediaPrint+"</style></head>";
            popupWin.document.open();
            popupWin.document.write('<html>' + vStyle + '<body onload="window.print()">' + divToPrint.innerHTML + '</html>');
            popupWin.document.close();
        }

        
    
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <!--Header start-->
    <div>
        <uc1:Header ID="Header1" runat="server" />
    </div>
    <!--Header End-->
    <!--Body Start-->
    <div class="wrapper">
        <div class="main-table-wrapper">
            <div class="breadcrmbLeft">
                <input type="hidden" name="hdnData" id="hdnData" />
                <div class="searchBox">
                    <asp:TextBox ID="txtSearch" value="Search" runat="server" class="searchBoxTxt" onkeypress="return SetDefaultButton(event,1);"
                        onfocus="if (this.value==&#39;Search&#39;) this.value=&#39;&#39;" onblur="if (this.value==&#39;&#39;) this.value=&#39;Search&#39;" />
                    <asp:LinkButton ID="lnkBtnSearch" class="searchBoxBtn" runat="server" OnClick="lnkBtnSearch_Click"
                        CausesValidation="False"></asp:LinkButton>
                    <div class="clear">
                    </div>
                </div>
                <div id="dvAddUser" class="fl">
                    <%--<span class="btn5"><a href="#nogo" onclick="PrepareForPrint('dvBarcodes')"><span class="PrintBarcode">
                    </span>Print</a></span>--%>
                   
                    <span class="btn5"><a href="../handler/BarcodePDF.aspx?q=<%=PurchaseID %>" target="_new"><span class="PrintPDF">
                    </span>Print to PDF</a></span>

                </div>
                <div class="reports">
                    Print | Product Barcode
                </div>
                <div class="clear">
                </div>
            </div>
            <span id="ContentPlaceHolder1_lblMsg"></span>
            <div id="updMain">
            <div id="sss"></div>
                <div id="dvgridcontainer" class="grid_container" style="width: 1100px;">
                    <!--Start GRID-->
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" ClientIDMode="Static">
                        <ContentTemplate>
                            <div style="margin: 0px auto; padding: 0px; text-align: center;">
                                <div id="divMess" runat="server" visible="false">
                                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                </div>
                            </div>
                            <br style="clear: both" />
                            <input type="text" id="ttt" style="display:none" />
                            <div class="grid_container" runat="server" id="dvBarcodes">
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <!--End Grid-->
                </div>
            </div>
        </div>
    </div>
    <div class="push" style="min-height:50%">
    </div>
    <div style="clear:both"></div>
    <!--Body End-->
    <!--Footer Start-->
    <div>
        <uc2:Footer ID="Footer1" runat="server" />
    </div>
    <!--Footer Start-->
    </form>
</body>
<script src="../Scripts/jquery-barcode-2.0.2.min.js" type="text/javascript"></script>
<script type="text/javascript">
    $(document).ready(function () {
        var i = 1;
        $(".Section").each(function (index, obj) {

            var str = $(obj).html();
            if (str == '') {
                $(obj).attr('style', 'background:red;');
            }
            else {
                if (str.indexOf("dvbarcode") !== -1) {
                    var divID = "dvbarcode" + i;
                    var PrintdivID = "dvbarcodePrint" + i;
                    generateBarcode($("#" + divID).html(), PrintdivID);
                    i++;
                }
            }
        });
    });


    function generateBarcode(barcodeValue, barcodePrint) {

        var values = barcodeValue;
        $("#ttt").val(values);
        values = $.trim($("#ttt").val());
        var btype = "code128";
        
        var settings = {
            output: "css",
            bgColor: "#FFFFFF",
            color: "#000000",
            barWidth: "1",//6
            barHeight: "30"//500 
        };

        $("#" + barcodePrint).html("").show().barcode(values, btype, settings);
        $("#ttt").val("");
    }


    function PrintBarCodeMultiple(ID) {
        var Quantity = $("#txtQuantity" + ID).val();
        var BarCode = $("#dvbarcode" + ID).html();
        BarCode = $.trim(BarCode);
        if (null != Quantity) {
            //alert(BarCode);
            location.href = "../handler/BarcodePDF.aspx?bc=" + BarCode + "&Qty=" + Quantity;
        }
    }


   

    

</script>
<style>
 .barcodeTarget{overflow:hidden!important;width:1100px!Important;}
</style>
</html>
