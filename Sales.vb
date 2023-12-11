Imports MySql.Data.MySqlClient
Imports PharmacyandMedicine.SalesWindow

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

                Dim query As String = "SELECT inventory_id, item_name, category, item_qty, item_price FROM Inventory"
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

                Dim query As String = "SELECT inventory_id, item_name, category, item_qty, item_price FROM Inventory WHERE LOWER(item_name) LIKE @keyword"
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

            ' Obtain or provide the salesId value here
            Dim salesId As Integer

            ' Logic to obtain the latest salesId from your database
            Try
                Using connection As New MySqlConnection(connectionString)
                    connection.Open()

                    Dim query As String = "SELECT MAX(sales_id) FROM compute_sales"
                    Using cmd As New MySqlCommand(query, connection)
                        Dim result As Object = cmd.ExecuteScalar()
                        If result IsNot Nothing AndAlso Not DBNull.Value.Equals(result) Then
                            salesId = Convert.ToInt32(result) + 1
                        Else
                            salesId = 1 ' If no salesId exists, start from 1
                        End If
                    End Using
                End Using
            Catch ex As Exception
                MessageBox.Show("Error obtaining salesId: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub ' Exit the sub if there's an error obtaining salesId
            End Try

            ' Retrieve dosage information from the INVENTORY table
            Dim itemDosage As String = RetrieveDosage(selectedItemName)

            ' Pass the salesId and itemDosage when creating ModalOrderSales2 instance
            Dim modalOrderForm As New ModalOrderSales2(selectedItemName, initialQuantity, salesId, itemDosage)
            modalOrderForm.ShowDialog()
        Else
            MessageBox.Show("Please select an item from the list before adding an order.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

    Private Function RetrieveDosage(itemName As String) As String
        Dim dosage As String = String.Empty

        Try
            Using connection As New MySqlConnection(connectionString)
                connection.Open()

                Dim query As String = "SELECT item_dosage FROM inventory WHERE item_name = @item_name"

                Using cmd As New MySqlCommand(query, connection)
                    cmd.Parameters.AddWithValue("@item_name", itemName)

                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            dosage = Convert.ToString(reader("item_dosage"))
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error retrieving dosage information: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return dosage
    End Function

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        If SalesWindow.salesWindowInstance Is Nothing OrElse SalesWindow.salesWindowInstance.IsDisposed Then
            SalesWindow.salesWindowInstance = New SalesWindow()
        End If

        SalesWindow.salesWindowInstance.Show()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        ReloadPOSData()
    End Sub

    ' Method to reload and refresh the DataGridView
    Private Sub ReloadPOSData()
        Try
            Using connection As New MySqlConnection(connectionString)
                connection.Open()

                Dim query As String = "SELECT inventory_id, item_name, category, item_qty, item_price FROM Inventory"
                Using cmd As New MySqlCommand(query, connection)
                    Using adapter As New MySqlDataAdapter(cmd)
                        ' Clear existing data
                        orderDataTable.Clear()

                        ' Fill with new data
                        adapter.Fill(orderDataTable)
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error reloading data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
