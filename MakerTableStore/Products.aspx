<%@ Page Language="C#" AutoEventWireup="true" Title="Products" MasterPageFile="~/Site.Master" CodeBehind="Products.aspx.cs" Inherits="MakerTableStore.Products" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="SiteText"><h2><%: Title %></h2>
    <h3>Choose from these examples or design your own!</h3></div>
    <div class="row col-xs-12">
        <asp:Button ID="ShoppingCartBtn" runat="server" Text="Check Out" OnClick="ShoppingCartBtn_Click" />
        <asp:Label ID="Label3" runat="server" Text="Order Status:   ">
            <asp:TextBox width="1000" ID="ErrorMessage" runat="server"></asp:TextBox>
        </asp:Label>
    </div>
    
    <div class="container">
        <div class="row">
            <div class="col-xs-4"><asp:Image src="https://img1.etsystatic.com/158/0/14530726/il_570xN.1208931555_jjnd.jpg" class="img-responsive" ID="Image7" runat="server" /></div>
            <div class="col-xs-4"><asp:Image src="https://img0.etsystatic.com/161/0/14530726/il_570xN.1160319320_gjzq.jpg" class="img-responsive" ID="Image13" runat="server" /></div>
            <div class="col-xs-4"><asp:Image src="https://img1.etsystatic.com/155/0/14530726/il_570xN.1241578349_j512.jpg" class="img-responsive" ID="Image14" runat="server" /></div>
        </div>

        <div class="row text-center">
            <div class="col-xs-4"><div class="SiteText" >Pork Cuts Sign - Custom Sizes Available</div></div>
            <div class="col-xs-4"><div class="SiteText" >Metal Nashville Sign</div></div>
            <div class="col-xs-4"><div class="SiteText" >Custom Metal Property Sign - YOUR TEXT</div></div>         
        </div>
        
        <div class="row">
            <div class="col-xs-4 col-xl-4 pull-right"><div class="SiteText" >
                <asp:Label ID="PorkCutLbl" runat="server" Text="Quantity">
                <asp:TextBox ID="PorkCutQty" width="40" runat="server"></asp:TextBox></asp:Label>
                <asp:Button ID="PorkCutBtn" CssClass="btn pull-right" runat="server" OnClick="PorkCutBtn_Click" Text="Add To Cart" />
           </div></div>
            <div class="col-xs-4"><div class="SiteText">
                <asp:Label ID="Label1" runat="server" Text="Quantity">
                <asp:TextBox ID="TextBox1"  width="40" runat="server"></asp:TextBox></asp:Label>
                <asp:Button ID="Button1" runat="server" Text="Add To Cart" />
            </div></div>
            <div class="col-xs-4"><div class="SiteText" >
                <asp:Label ID="Label2" runat="server" Text="Quantity">
                <asp:TextBox ID="TextBox2"  width="40" runat="server"></asp:TextBox></asp:Label>
                <asp:Button ID="Button2" runat="server" Text="Add To Cart" />
            </div></div>     
        </div>
        <div class="row">
            <div class="col-xs-4"><asp:Image src="https://img1.etsystatic.com/158/0/14530726/il_570xN.1208931555_jjnd.jpg" class="img-responsive" ID="Image1" runat="server" />Pork Cuts Sign - Custom Sizes Available</div>
            <div class="col-xs-4"><asp:Image src="https://img0.etsystatic.com/161/0/14530726/il_570xN.1160319320_gjzq.jpg" class="img-responsive" ID="Image2" runat="server" />Metal Nashville Sign</div>
            <div class="col-xs-4"><asp:Image src="https://img1.etsystatic.com/155/0/14530726/il_570xN.1241578349_j512.jpg" class="img-responsive" ID="Image3" runat="server" /> Custom Metal Property Sign - YOUR TEXT </div>
        </div>
        <div class="row">
            <div class="col-xs-4"><asp:Image src="https://img1.etsystatic.com/158/0/14530726/il_570xN.1208931555_jjnd.jpg" class="img-responsive" ID="Image4" runat="server" />Pork Cuts Sign - Custom Sizes Available</div>
            <div class="col-xs-4"><asp:Image src="https://img0.etsystatic.com/161/0/14530726/il_570xN.1160319320_gjzq.jpg" class="img-responsive" ID="Image5" runat="server" />Metal Nashville Sign</div>
            <div class="col-xs-4"><asp:Image src="https://img1.etsystatic.com/155/0/14530726/il_570xN.1241578349_j512.jpg" class="img-responsive" ID="Image6" runat="server" /> Custom Metal Property Sign - YOUR TEXT </div>
        </div>
        <div class="row">
            <div class="col-xs-4"><asp:Image src="https://img1.etsystatic.com/158/0/14530726/il_570xN.1208931555_jjnd.jpg" class="img-responsive" ID="Image10" runat="server" />Pork Cuts Sign - Custom Sizes Available</div>
            <div class="col-xs-4"><asp:Image src="https://img0.etsystatic.com/161/0/14530726/il_570xN.1160319320_gjzq.jpg" class="img-responsive" ID="Image11" runat="server" />Metal Nashville Sign</div>
            <div class="col-xs-4"><asp:Image src="https://img1.etsystatic.com/155/0/14530726/il_570xN.1241578349_j512.jpg" class="img-responsive" ID="Image12" runat="server" /> Custom Metal Property Sign - YOUR TEXT </div>
        </div>
    </div>
</asp:Content>
