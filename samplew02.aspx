<%@ Page Language="C#" AutoEventWireup="true" CodeFile="samplew02.aspx.cs" Inherits="samplew02" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <style>
        #grid-container {
            text-align: left; /* Aligns the GridView and the groupboxes to the left */
            width: 35%; /* Ensure the container takes the full width */
            margin-top:20px;
        }

        .gridview-table {
            table-layout: fixed; /* Ensures fixed column width */
            width: 100%;
        }

        .groupbox-container {
            display: flex;
            justify-content: space-between;
            gap: 5px; /* Reduced gap between the two groupboxes */
            margin-top: 10px;
        }

        .groupbox {
            width: 48%;
            box-sizing: border-box;
            height: 90px;
        }

        fieldset {
            border: 1px solid #000;
            padding: 10px;
             margin: 0;
            width: 100%;
            height: 100%; /* Ensures the content inside the fieldset stretches to fit the height */
            box-sizing: border-box;
            display: flex;
            flex-direction: column;
            align-items: flex-start; /* Align items (radio buttons) to the top-left */
            justify-content: flex-start; /* Make sure content starts from the top */
        }

     
    </style>

</head>
<body>
    <form id="form1" runat="server">
       <div id="grid-container">
         <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="gridview-table" AutoGenerateEditButton="false" 
            OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" 
            OnRowCancelingEdit="GridView1_RowCancelingEdit" OnSorting="GridView1_Sorting" AllowSorting="True">
            <Columns>
                <asp:CommandField ShowEditButton="True">
                    <ItemStyle Width="100px" /> 
                    <HeaderStyle Width="100px" />
                </asp:CommandField>

                <asp:TemplateField HeaderText="Id" SortExpression="Id">
                    <ItemStyle Width="150px" /> 
                    <HeaderStyle Width="150px" /> 
                    <ItemTemplate>
                        <%# Eval("Id") %> 
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox_Id" runat="server" Text='<%# Bind("Id") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Name" SortExpression="Name">
                    <ItemStyle Width="150px" /> 
                    <HeaderStyle Width="150px" /> 
                    <ItemTemplate>
                        <%# Eval("Name") %> 
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox_Name" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Town" SortExpression="Town">
                    <ItemStyle Width="150px" /> 
                    <HeaderStyle Width="150px" /> 
                    <ItemTemplate>
                        <%# Eval("Town") %>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox_Town" runat="server" Text='<%# Bind("Town") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Country" SortExpression="Country">
                    <ItemStyle Width="150px" /> 
                    <HeaderStyle Width="150px" /> 
                    <ItemTemplate>
                        <%# Eval("Country") %>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox_Country" runat="server" Text='<%# Bind("Country") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
       
       
            <div class="groupbox-container">
                <!-- Groupbox 1 -->
                <div class="groupbox" >
                    <fieldset>
                        <legend>Destination</legend>
                        <!-- Radio Buttons -->
                        <div>
                            <asp:RadioButton ID="RadioButton1" runat="server" GroupName="Output" Text="On screen" />
                        </div>

                        <div>
                            <asp:RadioButton ID="RadioButton2" runat="server" GroupName="Output" Text="On paper" />
                        </div>
                    </fieldset>
                </div>

                <!-- Groupbox 2 -->
                <div class="groupbox">
                    <fieldset>
                        <legend>Formula</legend>
                        <div>
                             Country = Germany
                        </div>
                        <br />
                       <div>
                             <asp:CheckBox ID="CheckBox1" runat="server" Text="Include formula"/>
                       </div>
                    </fieldset>
                </div>
            </div>
        

         <br />
            Sort:

        <div class="sort-container">
            <asp:Label ID="lblColumn" runat="server" Text="Column"></asp:Label>
               <br />
            <asp:Label ID="lblDirection" runat="server" Text="Direction"></asp:Label>
         </div>

               <br /><br />
        <asp:Button ID="Button1" runat="server" Text="Print" OnClick="btnPrint_Click"   />

        </div>

    </form>
</body>
</html>

<%--https://www.aspsnippets.com/questions/301333/Edit-GridView-cell-when-clicked-by-making-it-TextBox-and-update-new-value-in-database-using-jQuery-in-ASPNet-with-C-and-VBNet/--%>
