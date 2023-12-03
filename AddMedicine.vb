Imports MySql.Data.MySqlClient

Public Class AddMedicine
    ' Connection string for MySQL
    Private connectionString As String = "server=127.0.0.1;userid=root;password='';database=employees"

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialize the ComboBox with dosage units
        PopulateDosageUnitsComboBox()
        ComboBox1.SelectedIndex = 0
    End Sub

    Private Sub PopulateDosageUnitsComboBox()
        Try
            Using connection As MySqlConnection = New MySqlConnection(connectionString)
                connection.Open()

                Dim query As String = "SELECT UnitName FROM DosageUnits;"
                Using command As MySqlCommand = New MySqlCommand(query, connection)
                    Using reader As MySqlDataReader = command.ExecuteReader()
                        While reader.Read()
                            ComboBox1.Items.Add(reader("UnitName").ToString())
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ' Handle exceptions (e.g., display an error message)
            MessageBox.Show("Error fetching dosage units: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        ' Combo box (e.g., 500ml be like)
        Dim selectedDosage As String = ComboBox1.SelectedItem.ToString()
        Label5.Text = "Selected Dosage Unit: " & selectedDosage
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' Add operation to DB
        Try
            Using connection As MySqlConnection = New MySqlConnection(connectionString)
                connection.Open()

                ' Get the selected dosage unit ID from the DosageUnits table
                Dim dosageUnitID As Integer = GetDosageUnitID(ComboBox1.SelectedItem.ToString(), connection)

                ' Insert medicine into the medicine table
                Dim query As String = "INSERT INTO medicine (med_name, med_dosage, med_QTY, med_price, DosageUnitID) VALUES (@med_name, @med_dosage, @med_QTY, @med_price, @DosageUnitID);"
                Using command As MySqlCommand = New MySqlCommand(query, connection)
                    command.Parameters.AddWithValue("@med_name", TextBox1.Text)
                    command.Parameters.AddWithValue("@med_dosage", TextBox2.Text)
                    command.Parameters.AddWithValue("@med_QTY", Integer.Parse(TextBox3.Text))
                    command.Parameters.AddWithValue("@med_price", Integer.Parse(TextBox4.Text))
                    command.Parameters.AddWithValue("@DosageUnitID", dosageUnitID)

                    command.ExecuteNonQuery()

                    ' Optional: Display a success message
                    MessageBox.Show("Medicine added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                    ' Close the form
                    Me.Close()
                End Using
            End Using
        Catch ex As Exception
            ' Handle exceptions (e.g., display an error message)
            MessageBox.Show("Error adding medicine: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' Helper function to get the dosage unit ID
    Private Function GetDosageUnitID(dosageUnitName As String, connection As MySqlConnection) As Integer
        Dim query As String = "SELECT ID FROM DosageUnits WHERE UnitName = @UnitName;"
        Using command As MySqlCommand = New MySqlCommand(query, connection)
            command.Parameters.AddWithValue("@UnitName", dosageUnitName)

            Dim result As Object = command.ExecuteScalar()
            If result IsNot Nothing Then
                Return Convert.ToInt32(result)
            Else
                ' Default to 1 if the dosage unit ID is not found (you might want to handle this differently based on your requirements)
                Return 1
            End If
        End Using
    End Function
End Class
