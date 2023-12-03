Imports MySql.Data.MySqlClient

Public Class Sales
    Private connectionString As String = "server=127.0.0.1;userid=root;password='';database=employees"
    Private medicineBindingSource As New BindingSource()

    Private Sub LoadMedicines()
        Try
            Using connection As MySqlConnection = New MySqlConnection(connectionString)
                connection.Open()
                Dim query As String = "SELECT med_id, med_name, med_dosage, med_QTY, med_price, UnitName FROM medicine JOIN DosageUnits ON medicine.DosageUnitID = DosageUnits.ID;"

                Using command As MySqlCommand = New MySqlCommand(query, connection)
                    Using reader As MySqlDataReader = command.ExecuteReader()
                        LoadDataToDataGridView(reader)
                    End Using
                End Using
            End Using
        Catch ex As Exception
            HandleError("Error loading medicines", ex)
        End Try
    End Sub

    Private Sub LoadDataToDataGridView(reader As MySqlDataReader)
        Dim dataTable As New DataTable()
        dataTable.Load(reader)
        medicineBindingSource.DataSource = dataTable
        POSData1.DataSource = medicineBindingSource
    End Sub

    Private Sub HandleError(message As String, ex As Exception)
        MessageBox.Show($"{message}: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ' Log the error for future reference (optional)
        ' Logger.Log(ex)
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'dashboard
        Dim dashForm As New Dash()
        dashForm.Show()
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        'point of sale active
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

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click
        'total label
    End Sub

    Private Sub Label7_Click(sender As Object, e As EventArgs) Handles Label7.Click
        'total discount label
    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs) Handles Label8.Click
        'total amount label
    End Sub

    Private Sub Label9_Click(sender As Object, e As EventArgs) Handles Label9.Click
        'cash label
    End Sub

    Private Sub Label10_Click(sender As Object, e As EventArgs) Handles Label10.Click
        'change label
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        'search textbox
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        'search medicine label
    End Sub
End Class