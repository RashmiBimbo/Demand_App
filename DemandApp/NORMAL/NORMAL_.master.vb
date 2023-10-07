Imports System.Data
Imports System.Data.OleDb
Partial Class NORMAL_NORMAL_
    Inherits System.Web.UI.MasterPage

    Dim connect As New I_Connection.connection_I
    Dim connect1 As New I_Connection.connection_I
    Dim connect2 As New I_Connection.connection_I
    Dim connect3 As New I_Connection.connection_I
    Dim conn1 As String
    Dim conn2 As String
    Dim conn3 As String

    Dim dt As DataTable
    Dim msqlquery As String
    Dim L_location As String
    Dim T_Location As String

    'Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
    '    If (Request.ServerVariables("http_user_agent").IndexOf("Safari", StringComparison.CurrentCultureIgnoreCase) <> -1) Or (Request.ServerVariables("http_user_agent").IndexOf("Chrome", StringComparison.CurrentCultureIgnoreCase) <> -1) Then
    '        Page.ClientTarget = "uplevel"
    '    End If
    'End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LBLDATE.Text = Format(Now, "dddd, dd MMMM, yyyy ")

        conn2 = ConfigurationManager.ConnectionStrings("ConnectionString2").ConnectionString
        connect2.connect(conn2)

        'Oracle
        conn3 = ConfigurationManager.ConnectionStrings("ConnectionString2").ConnectionString
        connect3.connect(conn3)


        If Not Page.IsPostBack Then

            Me.N_Paycode.Text = Session.Item("Login_id").ToString.Trim.ToUpper
            Me.N_Sub_Company_id.Text = Session.Item("Sub_Company_Id").ToString.Trim.ToUpper
            Me.N_Location.Text = Session.Item("Location").ToString.Trim.ToUpper
            Me.N_Sub_Location.Text = Session.Item("Sub_Location").ToString.Trim.ToUpper

            If N_Sub_Company_id.Text = String.Empty = False Then

                If N_Location.Text = String.Empty = False Then

                    If N_Sub_Location.Text = String.Empty = True Then
                        L_location = " and Sub_Company_id= '" & N_Sub_Company_id.Text & "' And Location ='" & N_Location.Text & "'"
                    Else
                        L_location = " and Sub_Company_id= '" & N_Sub_Company_id.Text & "' And Location ='" & N_Location.Text & "' And Sub_Location='" & N_Sub_Location.Text & "'"
                    End If

                Else
                    ' L_location = ""
                    L_location = " and Sub_Company_id= '" & N_Sub_Company_id.Text & "'"

                End If

            Else
                L_location = ""

            End If

             msqlquery = "Select Distinct Name from V_Company_Master Where Dataareaid='" & Me.N_Sub_Company_id.Text & "'"
            dt = connect.SelQuery(msqlquery, connect2.conn)
            If dt.Rows.Count > 0 Then
                Me.Label1.Text = dt.Rows.Item(0)(0).ToString.ToUpper.Trim
            End If

            get_menuitem(N_Paycode.Text, L_location)

            'If Request.UserAgent.IndexOf("AppleWebKit") > 0 Then
            '    Request.Browser.Adapters.Clear()
            'End If
        End If


      

    End Sub
  
    
    'Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
    '    If (Request.ServerVariables("http_user_agent").IndexOf("Safari", StringComparison.CurrentCultureIgnoreCase) <> -1) Or (Request.ServerVariables("http_user_agent").IndexOf("Chrome", StringComparison.CurrentCultureIgnoreCase) <> -1) Then
    '        Page.ClientTarget = "uplevel"
    '    End If
    'End Sub
    Sub get_menuitem(ByVal login_id As String, ByVal location As String)

        msqlquery = "select Leaf_id,Leaf_Name,Path,(select count(*) FROM Sd_USer_Permission_Master " _
       & "WHERE parent_id=sc.Leaf_id) childnodecount FROM SD_USer_Permission_Master sc Where Parent_Id is null and isactive=1 and Login_id='" & login_id & "' " & L_location & "  order by Login_id,leaf_id "
        dt = connect2.SelQuery(msqlquery, connect2.conn)

        Dim view As New DataView(dt)

        For Each row As DataRowView In view

            Dim menuItem As New MenuItem(row("Leaf_Name").ToString(), row("Leaf_Id").ToString())

            menuItem.NavigateUrl = row("path").ToString()

            Menu1.Items.Add(menuItem)

            AddChildItems(dt, menuItem, login_id, location)
        Next


    End Sub

    Private Sub AddChildItems(ByVal table As DataTable, ByVal menuItem As MenuItem, ByVal login_id As String, ByVal location As String)
        Dim msqlquery As String
        'viewItem.RowFilter = "Leaf_Id = " & Convert.ToString(menuItem.Value)

        msqlquery = "select Leaf_id,Leaf_Name,Path,(select count(*) FROM SD_USer_Permission_Master " _
            & "WHERE parent_id=sc.Leaf_id) childnodecount FROM SD_USer_Permission_Master sc where parent_ID=" & Convert.ToString(menuItem.Value) & " and isactive=1 and Login_id='" & login_id & "' " & location & "  order by Login_id,leaf_id "
        dt = connect2.SelQuery(msqlquery, connect2.conn)

        Dim viewItem As New DataView(dt)

        For Each childView As DataRowView In viewItem
            Dim childItem As New MenuItem(childView("Leaf_Name").ToString(), childView("Leaf_Id").ToString())
            childItem.NavigateUrl = childView("path").ToString()
            menuItem.ChildItems.Add(childItem)
            AddChildItems(dt, childItem, login_id, location)
        Next
    End Sub

    Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
        Session.RemoveAll()
        Response.Redirect("~/default.aspx")
    End Sub

    Protected Sub ImageButton2_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImageButton2.Click
        Response.Redirect("~/home.aspx")
    End Sub

    'Protected Sub txtchat_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtchat.TextChanged

    '    Dim mSqlQuery As String
    '    'StrConv(Title, VbStrConv.ProperCase)
    '    Dim strIPAddress As String
    '    strIPAddress = HttpContext.Current.Request.UserHostAddress

    '    If Trim(txtchat.Text) = String.Empty Or Trim(txtchat.Text) = "  " Then

    '    Else
    '        txtchat.Text = Replace(Trim(txtchat.Text), "'", "''")

    '        mSqlQuery = "insert into Tbl_Comments(Login_id,s_date,msg,ip_address) values('" & Trim(Session.Item("Login_Id")) & "','" & Format(Now, "dd-MMM-yyyy HH:mm:ss") & "','" & txtchat.Text & "','" & strIPAddress & "')"
    '        connect2.OtherQuery(mSqlQuery, connect2.conn)

    '        'TextBox1.Text = TextBox1.Text + vbNewLine + Session.Item("Login_Name") + " : " + TextBox2.Text
    '        'txtchat.Text = String.Empty
    '        'txtchat.Focus()
    '    End If
    '    txtchat.Text = String.Empty
    '    ' txtchat.Text = ""
    '    txtchat.Focus()
    'End Sub

End Class

