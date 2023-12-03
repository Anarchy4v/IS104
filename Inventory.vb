Imports MySql.Data.MySqlClient

Public Class Inventory
    ' Connection string for MySQL
    Private connectionString As String = "server=127.0.0.1;userid=root;password='';database=employees"

    Private Sub Inventory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Load medicines into DataGridView
        LoadMedicines()
    End Sub

    Private Sub LoadMedicines()
        Try
            Using connection As MySqlConnection = New MySqlConnection(connectionString)
                connection.Open()

                Dim query As String = "SELECT med_id, med_name, med_dosage, med_QTY, med_price, UnitName FROM medicine JOIN DosageUnits ON medicine.DosageUnitID = DosageUnits.ID;"
                Using adapter As MySqlDataAdapter = New MySqlDataAdapter(query, connection)
                    Dim dataTable As New DataTable()
                    adapter.Fill(dataTable)

                    ' Set the DataTable as the DataGridView's DataSource
                    DataGridView1.DataSource = dataTable
                End Using
            End Using
        Catch ex As Exception
            ' Handle exceptions (e.g., display an error message)
            MessageBox.Show("Error loading medicines: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        'search medicine
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

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'inventory active
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        'sales report
        Dim salesReport As New SalesReport()
        salesReport.Show()
        Me.Close()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        'setting
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        'log out
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to log out?", "Log Out", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            Me.Close()
            Form1.Show()
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        'category list
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        ' Delete selected medicine from database
        If DataGridView1.SelectedRows.Count > 0 Then
            Dim selectedRowIndex As Integer = DataGridView1.SelectedRows(0).Index
            Dim medId As Integer = Convert.ToInt32(DataGridView1.Rows(selectedRowIndex).Cells("med_id").Value)

            Try
                Using connection As MySqlConnection = New MySqlConnection(connectionString)
                    connection.Open()

                    Dim query As String = "DELETE FROM medicine WHERE med_id = @medId;"
                    Using command As MySqlCommand = New MySqlCommand(query, connection)
                        command.Parameters.AddWithValue("@medId", medId)
                        command.ExecuteNonQuery()
                    End Using
                End Using

                ' Refresh DataGridView to reflect changes
                LoadMedicines()
                MessageBox.Show("Medicine deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show("Error deleting medicine: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
            MessageBox.Show("Please select a medicine to delete.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub


    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        'edit medicine in data table view
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        'add medicine in data table view
        Dim addMed As New AddMedicine()
        addMed.Show()
    End Sub
End Class