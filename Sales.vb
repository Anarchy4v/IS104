Imports MySql.Data.MySqlClient

Public Class Sales
    Private connectionString As String = "server=127.0.0.1;userid=root;password='';database=tgp_db"
    Private orderDataTable As New DataTable()

    Private Sub NavigateToForm(form As Form)
        form.Show()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim dashForm As New Dash()
        dashForm.Show()
        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim Inventor As New Inventory()
        Inventor.Show()
        Me.Close()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to log out?", "Log Out", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            Dim Login As New Form1()
            Login.Show()
            Me.Close()
        End If
    End Sub

    Private Sub Sales_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        POSData1.DataSource = orderDataTable

        Try
            Using connection As New MySqlConnection(connectionString)
                connection.Open()

                Dim query As String = "SELECT inventory_id, item_name, item_qty, item_price FROM Inventory"
                Using cmd As New MySqlCommand(query, connection)
                    Using adapter As New MySqlDataAdapter(cmd)
                        adapter.Fill(orderDataTable)
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Dim keyword As String = TextBox1.Text.ToLower()

        Try
            Using connection As New MySqlConnection(connectionString)
                connection.Open()

                Dim query As String = "SELECT item_name FROM Inventory WHERE LOWER(item_name) LIKE @keyword"
                Using cmd As New MySqlCommand(query, connection)
                    cmd.Parameters.AddWithValue("@keyword", "%" & keyword & "%")

                    Using adapter As New MySqlDataAdapter(cmd)
                        orderDataTable.Clear()
                        adapter.Fill(orderDataTable)
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error searching for data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        If POSData1.SelectedRows.Count > 0 Then
            Dim selectedItemName As String = POSData1.SelectedRows(0).Cells("item_name").Value.ToString()

            If SalesWindow.salesWindowInstance Is Nothing OrElse SalesWindow.salesWindowInstance.IsDisposed Then
                SalesWindow.salesWindowInstance = New SalesWindow()
            End If

            Dim initialQuantity As Integer = 1
            Dim modalOrderForm As New ModalOrderSales2(selectedItemName, initialQuantity)
            modalOrderForm.ShowDialog()

            If modalOrderForm.DialogResult = DialogResult.OK Then
                Dim orderDetails As SalesWindow.OrderDetails = modalOrderForm.OrderDetails

                If SalesWindow.salesWindowInstance IsNot Nothing AndAlso Not SalesWindow.salesWindowInstance.IsDisposed Then
                    SalesWindow.salesWindowInstance.AddOrderToDataTable(orderDetails.ItemName, orderDetails.Quantity, orderDetails.Price)
                Else
                    MessageBox.Show("SalesWindow is not available.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If
        Else
            MessageBox.Show("Please select an item from the list before adding an order.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        If SalesWindow.salesWindowInstance Is Nothing OrElse SalesWindow.salesWindowInstance.IsDisposed Then
            SalesWindow.salesWindowInstance = New SalesWindow()
        End If

        SalesWindow.salesWindowInstance.Show()
    End Sub
End Class
