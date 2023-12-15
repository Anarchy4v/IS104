Imports MySql.Data.MySqlClient

Public Class EditMedicine
    Private connectionString As String = "server=127.0.0.1;userid=root;password='';database=tgp_db"
    Private inventoryId As Integer

    ' Add a method to set the inventoryId for the selected medicine
    Public Sub SetInventoryId(id As Integer)
        inventoryId = id
    End Sub

    ' Add code to load and display medicine details based on inventoryId
    Public Sub LoadMedicineDetails()
        Try
            ' Initialize options for dosage unit (ComboBox1)
            ComboBox1.Items.Clear()
            ComboBox1.Items.Add("Grams")
            ComboBox1.Items.Add("Milligrams")
            ComboBox1.Items.Add("Millilitres")
            ComboBox1.Items.Add("Liter")
            ComboBox1.Items.Add("Ratio")
            ComboBox1.Items.Add("Percentage")

            ' Initialize options for medicine category (ComboBox2)
            ComboBox2.Items.Clear()
            ComboBox2.Items.Add("ANTIBIOTICS")
            ComboBox2.Items.Add("GASTROINTESTINAL")
            ComboBox2.Items.Add("STOMACH")
            ComboBox2.Items.Add("ANTIHISTAMINE")
            ComboBox2.Items.Add("ASTHMA")
            ComboBox2.Items.Add("ANTI-VERTIGO")

            Using connection As MySqlConnection = New MySqlConnection(connectionString)
                connection.Open()

                Dim query As String = "SELECT * FROM Inventory WHERE inventory_id = @inventoryId"

                Using command As MySqlCommand = New MySqlCommand(query, connection)
                    command.Parameters.AddWithValue("@inventoryId", inventoryId)

                    Using reader As MySqlDataReader = command.ExecuteReader()
                        If reader.Read() Then
                            ' Set values to the controls in the form
                            TextBox1.Text = reader("item_name").ToString()
                            ComboBox1.Text = reader("item_dosage").ToString()
                            ComboBox2.Text = reader("category").ToString()
                            TextBox3.Text = reader("item_qty").ToString()
                            TextBox4.Text = reader("item_price").ToString()
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ' Handle any exceptions here
            MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub UpdateMedicineDetails()
        Try
            Using connection As MySqlConnection = New MySqlConnection(connectionString)
                connection.Open()

                Dim query As String = "UPDATE Inventory SET item_name = @item_name, item_dosage = @item_dosage, category = @category, item_qty = @item_qty, item_price = @item_price WHERE inventory_id = @inventoryId"

                Using command As MySqlCommand = New MySqlCommand(query, connection)
                    command.Parameters.AddWithValue("@inventoryId", inventoryId)
                    command.Parameters.AddWithValue("@item_name", TextBox1.Text)
                    command.Parameters.AddWithValue("@item_dosage", ComboBox1.Text)
                    command.Parameters.AddWithValue("@category", ComboBox2.Text)
                    command.Parameters.AddWithValue("@item_qty", Convert.ToInt32(TextBox3.Text))
                    command.Parameters.AddWithValue("@item_price", Convert.ToDecimal(TextBox4.Text))
                    command.ExecuteNonQuery()

                    ' Optionally display a success message
                    MessageBox.Show("Medicine details updated successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End Using
            End Using
        Catch ex As Exception
            ' Handle any exceptions here
            MessageBox.Show("An error occurred: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button2.Click
        UpdateMedicineDetails()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Close the form without saving changes
        Me.Close()
    End Sub
End Class
