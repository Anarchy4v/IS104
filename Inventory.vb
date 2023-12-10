Imports MySql.Data.MySqlClient

Public Class Inventory
    Private connectionString As String = "server=127.0.0.1;userid=root;password='';database=tgp_db"
    Private medicineBindingSource As New BindingSource()

    Private Sub Inventory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Load data from the Inventory table into DataGridView1
        LoadInventoryData()
    End Sub

    Private Function GetOrderDetails() As DataTable
        ' Replace this with the actual code to retrieve order details from your database or another source
        Try
            Dim connectionString As String = "server=127.0.0.1;userid=root;password='';database=tgp_db"

            Using connection As MySqlConnection = New MySqlConnection(connectionString)
                connection.Open()

                Dim query As String = "SELECT * FROM OrderDetails"

                Using command As MySqlCommand = New MySqlCommand(query, connection)
                    Using reader As MySqlDataReader = command.ExecuteReader()
                        Dim dataTable As New DataTable()
                        dataTable.Load(reader)

                        Return dataTable
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error retrieving order details: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return Nothing
        End Try
    End Function

    Private Sub LoadInventoryData()
        Try
            Using connection As MySqlConnection = New MySqlConnection(connectionString)
                connection.Open()

                Dim query As String = "SELECT * FROM Inventory"

                Using command As MySqlCommand = New MySqlCommand(query, connection)
                    Using adapter As New MySqlDataAdapter(command)
                        Dim dataTable As New DataTable()
                        adapter.Fill(dataTable)

                        ' Set the DataSource for DataGridView1
                        DataGridView1.DataSource = dataTable
                        DataGridView1.Columns("inventory_id").Visible = True
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ' Handle any exceptions here
            MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SearchMedicine(searchText As String)
        Try
            Using connection As MySqlConnection = New MySqlConnection(connectionString)
                connection.Open()

                Dim query As String = "SELECT * FROM Inventory WHERE LOWER(item_name) LIKE @searchText"
                Using command As MySqlCommand = New MySqlCommand(query, connection)
                    command.Parameters.AddWithValue("@searchText", "%" & searchText & "%")

                    Using adapter As New MySqlDataAdapter(command)
                        Dim dataTable As New DataTable()
                        adapter.Fill(dataTable)

                        DataGridView1.DataSource = dataTable
                        DataGridView1.Columns("inventory_id").Visible = True
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'dashboard
        Dim dashForm As New Dash()
        dashForm.Show()
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'point of sale
        Dim sales As New Sales()
        sales.Show()
        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs)
        'inventory active
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ' Assuming you have a function to retrieve order details, replace it with the actual code
        Dim orderDetails As DataTable = GetOrderDetails()

        ' Open SalesReport form and pass the order details
        Dim salesReportForm As New SalesReport(orderDetails)
        salesReportForm.Show()

        ' Close the current Dash form
        Me.Close()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        'log out
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to log out?", "Log Out", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            Me.Close()
            Form1.Show()
        End If
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        ' Edit medicine in data table view
        ' Check if a row is selected
        If DataGridView1.SelectedRows.Count > 0 Then
            ' Get the selected inventory ID from the DataGridView
            Dim selectedInventoryId As Integer = Convert.ToInt32(DataGridView1.SelectedRows(0).Cells("inventory_id").Value)
            ' Open the EditMedicine form
            Dim editMedicineForm As New EditMedicine()
            editMedicineForm.SetInventoryId(selectedInventoryId)
            editMedicineForm.LoadMedicineDetails()
            editMedicineForm.Show()
        Else
            ' Display a message if no row is selected
            MessageBox.Show("Please select a medicine to edit.", "Edit Medicine", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        'add medicine in data table view
        Dim addMed As New AddMedicine()
        addMed.Show()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        ' Delete selected medicine
        If DataGridView1.SelectedRows.Count > 0 Then
            Dim result As DialogResult = MessageBox.Show("Are you sure you want to delete this medicine?", "Delete Medicine", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If result = DialogResult.Yes Then
                Dim selectedRow As DataGridViewRow = DataGridView1.SelectedRows(0)
                Dim inventoryId As Integer = Convert.ToInt32(selectedRow.Cells("inventory_id").Value)

                Try
                    Using connection As MySqlConnection = New MySqlConnection(connectionString)
                        connection.Open()

                        Dim query As String = "DELETE FROM Inventory WHERE inventory_id = @inventoryId"

                        Using command As MySqlCommand = New MySqlCommand(query, connection)
                            command.Parameters.AddWithValue("@inventoryId", inventoryId)
                            command.ExecuteNonQuery()

                            ' Refresh the DataGridView after deletion
                            LoadInventoryData()
                        End Using
                    End Using
                Catch ex As Exception
                    ' Handle any exceptions here
                    MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        Else
            MessageBox.Show("Please select a medicine to delete.", "Delete Medicine", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        ' Search medicine based on the entered text
        SearchMedicine(TextBox1.Text)
    End Sub
End Class