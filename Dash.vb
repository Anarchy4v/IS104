Imports MySql.Data.MySqlClient

Public Class Dash
    Dim random As New Random()
    Private formClosingByButton As Boolean = False
    Private formClosingBySystem As Boolean = False

    Private Sub Dash_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Start the timer when the form loads
        Timer1.Start()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'dashboard active
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'point of sales
        Dim sales As New Sales()
        sales.Show()
        Me.Close()
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs)
        'idk this
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs)

    End Sub

    'dashboard text

    Private Sub Label2_Click(sender As Object, e As EventArgs) Handles Label2.Click
        'retrive userID guess?
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)
        'idk who's the data source?
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'inventory
        Dim inv As New Inventory()
        inv.Show()
        Me.Close()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        'sales report
        Dim salesReport As New SalesReport()
        salesReport.Show()
        Me.Close()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        'settings
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to log out?", "Log Out", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            Me.Close()
            Form1.Show()
        End If
    End Sub


    Private Sub TextBox1_TextChanged_1(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        'search box???
    End Sub

    Private Sub DataGridView1_CellContentClick_1(sender As Object, e As DataGridViewCellEventArgs)
        'data grid view
    End Sub



    Private Sub frmSignUp_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        If Not formClosingBySystem Then
            Dim result As DialogResult = MessageBox.Show("Exit Application?", "IS104 - TGPharmacy", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            e.Cancel = (result = DialogResult.No)
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        ' Generate syempre live count haha
        Dim randomNumber As Integer = random.Next(1, 301)
        Dim randomNumber1 As Integer = random.Next(1, 401)
        Dim randomNumber2 As Integer = random.Next(1, 501)
        Dim randomNumber3 As Integer = random.Next(1, 201)

        Label7.Text = randomNumber.ToString()
        Label8.Text = randomNumber1.ToString()
        Label9.Text = randomNumber2.ToString()
        Label10.Text = randomNumber3.ToString()
    End Sub
End Class