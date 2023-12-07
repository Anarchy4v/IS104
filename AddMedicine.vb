﻿Imports MySql.Data.MySqlClient

Public Class AddMedicine
    Private connectionString As String = "server=127.0.0.1;userid=root;password='';database=tgp_db"

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        PopulateDosageUnitsComboBox()
        PopulateMedicineCategoryComboBox()
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
            MessageBox.Show("Error fetching dosage units: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub PopulateMedicineCategoryComboBox()
        Try
            Using connection As MySqlConnection = New MySqlConnection(connectionString)
                connection.Open()

                Dim query As String = "SELECT med_category FROM category;"
                Using command As MySqlCommand = New MySqlCommand(query, connection)
                    Using reader As MySqlDataReader = command.ExecuteReader()
                        While reader.Read()
                            ComboBox2.Items.Add(reader("med_category").ToString())
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error fetching medicine categories: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try
            Using connection As MySqlConnection = New MySqlConnection(connectionString)
                connection.Open()

                ' Get the DosageUnitID from the ComboBox
                Dim dosageUnitID As Integer = GetDosageUnitID(ComboBox1.SelectedItem.ToString(), connection)

                ' Get the med_category ID from the ComboBox
                Dim medCategoryID As Integer = GetMedCategoryID(ComboBox2.SelectedItem.ToString(), connection)

                Dim query As String = "INSERT INTO medicine (med_name, med_dosage, med_QTY, med_price, DosageUnitID, med_category) VALUES (@med_name, @med_dosage, @med_QTY, @med_price, @DosageUnitID, @med_category);"
                Using command As MySqlCommand = New MySqlCommand(query, connection)
                    command.Parameters.AddWithValue("@med_name", TextBox1.Text)
                    command.Parameters.AddWithValue("@med_dosage", TextBox2.Text)
                    command.Parameters.AddWithValue("@med_QTY", Integer.Parse(TextBox3.Text))
                    command.Parameters.AddWithValue("@med_price", Integer.Parse(TextBox4.Text))
                    command.Parameters.AddWithValue("@DosageUnitID", dosageUnitID)
                    command.Parameters.AddWithValue("@med_category", medCategoryID)

                    command.ExecuteNonQuery()

                    MessageBox.Show("Medicine added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Me.Close()
                End Using
            End Using
        Catch ex As MySqlException
            MessageBox.Show("MySQL Error: " & ex.Number & " - " & ex.Message, "MySQL Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Catch ex As Exception
            MessageBox.Show("Error adding medicine: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function GetMedCategoryID(medCategoryName As String, connection As MySqlConnection) As Integer
        Dim query As String = "SELECT ID FROM category WHERE med_category = @medCategoryName;"
        Using command As MySqlCommand = New MySqlCommand(query, connection)
            command.Parameters.AddWithValue("@medCategoryName", medCategoryName)

            Dim result As Object = command.ExecuteScalar()
            If result IsNot Nothing Then
                Return Convert.ToInt32(result)
            Else
                Return 1
            End If
        End Using
    End Function

    Private Function GetDosageUnitID(dosageUnitName As String, connection As MySqlConnection) As Integer
        Dim query As String = "SELECT ID FROM DosageUnits WHERE UnitName = @UnitName;"
        Using command As MySqlCommand = New MySqlCommand(query, connection)
            command.Parameters.AddWithValue("@UnitName", dosageUnitName)

            Dim result As Object = command.ExecuteScalar()
            If result IsNot Nothing Then
                Return Convert.ToInt32(result)
            Else
                Return 1
            End If
        End Using
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'log out
        Dim result As DialogResult = MessageBox.Show("Cancel operation?", "Cancel Operation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            Me.Close()
        End If
    End Sub
End Class
