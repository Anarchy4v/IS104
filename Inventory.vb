﻿Imports MySql.Data.MySqlClient

Public Class Inventory
    Private connectionString As String = "server=127.0.0.1;userid=root;password='';database=tgp_db"
    Private medicineBindingSource As New BindingSource()
    Private Sub Inventory_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LoadMedicines()
        AddHandler DataGridView1.CellFormatting, AddressOf DataGridView1_CellFormatting
    End Sub

    Private Sub LoadMedicines()
        Try
            Using connection As MySqlConnection = New MySqlConnection(connectionString)
                connection.Open()

                Dim query As String = "SELECT med_id, med_name, med_dosage, med_QTY, med_price, category.med_category AS CategoryName, UnitName FROM medicine JOIN DosageUnits ON medicine.DosageUnitID = DosageUnits.ID JOIN category ON medicine.med_category = category.ID;"
                Using adapter As MySqlDataAdapter = New MySqlDataAdapter(query, connection)
                    Dim dataTable As New DataTable()
                    adapter.Fill(dataTable)

                    medicineBindingSource.DataSource = dataTable
                    DataGridView1.DataSource = medicineBindingSource
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error loading medicines: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub DataGridView1_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles DataGridView1.CellFormatting
        If e.ColumnIndex = DataGridView1.Columns("med_price").Index AndAlso e.Value IsNot Nothing Then
            ' Check if the value is a numeric value
            Dim numericValue As Decimal
            If Decimal.TryParse(e.Value.ToString(), numericValue) Then
                e.Value = $"₱{numericValue:N2}"
                e.FormattingApplied = True
            End If
        End If
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

    Private Sub Button3_Click(sender As Object, e As EventArgs)
        'inventory active
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        'sales report
        Dim salesReport As New SalesReport()
        salesReport.Show()
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

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        'category list
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
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

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        'search medicine
    End Sub
End Class