﻿Imports MySql.Data.MySqlClient

Public Class Inventory
    Private connectionString As String = "server=127.0.0.1;userid=root;password='';database=tgp_db"
    Private medicineBindingSource As New BindingSource()

    Private Sub Inventory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadInventoryData()
    End Sub

    Private Sub LoadInventoryData()
        Try
            Using connection As MySqlConnection = New MySqlConnection(connectionString)
                connection.Open()

                Dim query As String = "SELECT * FROM Inventory"

                Using command As MySqlCommand = New MySqlCommand(query, connection)
                    Using adapter As New MySqlDataAdapter(command)
                        Dim dataTable As New DataTable()
                        adapter.Fill(dataTable)
                        DataGridView1.Columns.Clear()
                        DataGridView1.Columns.Add("inventory_id", "inventory_id")
                        DataGridView1.Columns.Add("MedicineName", "Medicine Name")
                        DataGridView1.Columns.Add("MedicineDosage", "Medicine Dosage")
                        DataGridView1.Columns.Add("MedicineCategory", "Medicine Category")
                        DataGridView1.Columns.Add("MedicineQuantity", "Medicine Quantity")
                        DataGridView1.Columns.Add("MedicinePrice", "Medicine Price")
                        For Each row As DataRow In dataTable.Rows
                            DataGridView1.Rows.Add(
                            row("inventory_id"),
                            row("item_name"),
                            row("item_dosage"),
                            row("category"),
                            row("item_qty"),
                            row("item_price")
                        )
                        Next
                    End Using
                End Using

                Dim userEmail As String = GetUserEmail(connection)
                Label2.Text = userEmail
            End Using
        Catch ex As Exception
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

                        ' Clear existing columns before adding new ones
                        DataGridView1.Columns.Clear()
                        ' Set the DataSource for DataGridView1
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
        'sales report
        Dim SalesReport As New SalesReport()
        SalesReport.Show()
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
        ' Check if a row is selected in your DataGridView (assuming you have one)
        If DataGridView1.SelectedRows.Count > 0 Then
            ' Get the selected inventory_id from the DataGridView (adjust column index as needed)
            Dim selectedInventoryId As Integer = Convert.ToInt32(DataGridView1.SelectedRows(0).Cells("inventory_id").Value)

            ' Instantiate the EditMedicine form
            Dim editMedicineForm As New EditMedicine()

            ' Set the inventoryId for the selected medicine
            editMedicineForm.SetInventoryId(selectedInventoryId)

            ' Load and display medicine details
            editMedicineForm.LoadMedicineDetails()

            ' Show the EditMedicine form
            editMedicineForm.ShowDialog()
        Else
            MessageBox.Show("Please select a medicine to edit.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub


    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Dim addMed As New AddMedicine()
        addMed.Show()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
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
                            LoadInventoryData()
                        End Using
                    End Using
                Catch ex As Exception
                    MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        Else
            MessageBox.Show("Please select a medicine to delete.", "Delete Medicine", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Function GetUserEmail(connection As MySqlConnection) As String
        ' Fetch the email from the usercredentials table
        Dim userEmail As String = String.Empty

        Dim query As String = "SELECT email FROM usercredentials WHERE user_id = @userId"
        Using command As MySqlCommand = New MySqlCommand(query, connection)
            ' Assuming you have a user_id associated with the current user
            ' Replace 1 with the actual user_id or parameterize it as needed
            command.Parameters.AddWithValue("@userId", 1)

            Using reader As MySqlDataReader = command.ExecuteReader()
                If reader.Read() Then
                    userEmail = reader("email").ToString()
                End If
            End Using
        End Using

        Return userEmail
    End Function

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        SearchMedicine(TextBox1.Text)
    End Sub
End Class