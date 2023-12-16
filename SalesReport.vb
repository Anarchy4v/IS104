Imports MySql.Data.MySqlClient

Public Module SharedSales
    Public SalesAmount As Decimal
    Public SalesTax As Decimal
    Public SalesTotal As Decimal
    Public OrderSalesID As Integer
End Module

Public Class SalesReport
    Private connectionString As String = "server=127.0.0.1;userid=root;password='';database=tgp_db"

    Private Sub SalesReport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim userId As Integer = 1

        ' Create a MySqlConnection with the connection string
        Using connection As New MySqlConnection(connectionString)
            ' Open the connection
            connection.Open()

            ' Define the SQL query to retrieve data for a specific user_id
            Dim query As String = "SELECT * FROM salesreport WHERE user_id = @user_id"

            ' Create a MySqlCommand with the query and connection
            Using command As New MySqlCommand(query, connection)
                ' Add the user_id parameter to the command
                command.Parameters.AddWithValue("@user_id", userId)

                ' Create a DataAdapter to execute the query and fill a DataSet
                Using adapter As New MySqlDataAdapter(command)
                    Dim dataSet As New DataSet()
                    ' Fill the DataSet with the result of the query
                    adapter.Fill(dataSet)

                    ' Display the data in a DataGridView or any other control as needed
                    ' For example, if you have a DataGridView named dataGridView1:
                    DataGridView1.DataSource = dataSet.Tables(0)

                    ' Compute the total of @total_price column
                    Dim total As Decimal = 0
                    Dim orderTotal As Integer = 0

                    For Each row As DataGridViewRow In DataGridView1.Rows
                        ' Check if the cell value is not empty and is a valid decimal
                        If Not row.Cells("total_price").Value Is Nothing AndAlso IsNumeric(row.Cells("total_price").Value) Then
                            total += Convert.ToDecimal(row.Cells("total_price").Value)
                            orderTotal += Convert.ToInt32(row.Cells("order_id").Value)
                        End If
                    Next

                    Dim vatPercentage As Decimal = 0.12 ' 12%
                    Dim vatAmount As Decimal = total * vatPercentage
                    Dim userEmail As String = GetUserEmail(connection)
                    Dim salesTotal As Decimal = vatAmount + total
                    Dim salesID As Integer = orderTotal

                    ' Update the SharedSales module variables
                    SharedSales.SalesAmount = total
                    SharedSales.SalesTax = vatAmount
                    SharedSales.SalesTotal = salesTotal
                    SharedSales.OrderSalesID = salesID

                    Label11.Text = userEmail
                    Label6.Text = total.ToString("C", New System.Globalization.CultureInfo("en-PH"))
                    Label7.Text = vatAmount.ToString("C", New System.Globalization.CultureInfo("en-PH"))
                    Label8.Text = salesTotal.ToString("C", New System.Globalization.CultureInfo("en-PH"))
                End Using
            End Using
        End Using
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'dashboard
        Dim dashForm As New Dash()
        dashForm.Show()
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'point of sales
        Dim sales As New Sales()
        sales.Show()
        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'inventory
        Dim inv As New Inventory()
        inv.Show()
        Me.Close()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to log out?", "Log Out", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            Me.Close()
            Form1.Show()
        End If
    End Sub

    Private Function GetUserEmail(connection As MySqlConnection) As String
        ' Fetch the email from the usercredentials table
        Dim userEmail As String = String.Empty

        Dim query As String = "SELECT email FROM usercredentials WHERE user_id = @userId"
        Using command As MySqlCommand = New MySqlCommand(query, connection)
            command.Parameters.AddWithValue("@userId", 1)

            Using reader As MySqlDataReader = command.ExecuteReader()
                If reader.Read() Then
                    userEmail = reader("email").ToString()
                End If
            End Using
        End Using

        Return userEmail
    End Function

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        Dim SalesGenerateReport As New SalesGenerateReport()
        SalesGenerateReport.Show()
    End Sub
End Class