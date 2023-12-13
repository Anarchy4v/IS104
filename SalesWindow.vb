﻿Imports System.Data.SqlClient
Imports System.Windows.Forms.VisualStyles.VisualStyleElement.Button
Imports MySql.Data.MySqlClient
Imports Mysqlx

Public Class SalesWindow
    Private connectionString As String = "server=127.0.0.1;userid=root;password='';database=tgp_db"
    Public Shared salesWindowInstance As SalesWindow
    Private totalAmount As Decimal
    Private totalSalesPrice As Decimal

    'this is needed because it references to my ModalOrderSales2
    Public Class OrderDetails
        Public Property ItemName As String
        Public Property Quantity As Integer
        Public Property Dosage As String
        Public Property Price As Decimal
        Public Property SalesId As Integer
    End Class

    ' Add a method to retrieve and display compute_sales data
    Private Sub DisplayComputeSalesData()
        Try
            Using connection As New MySqlConnection(connectionString)
                connection.Open()

                Dim query As String = "SELECT sales_id, sales_name, sales_dosage, sales_qty, sales_price FROM compute_sales"

                Using cmd As New MySqlCommand(query, connection)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        ' Assuming you have a DataGridView named dgvComputeSales in your form
                        DataGridView1.Rows.Clear()
                        Dim totalSalesPrice As Decimal = 0

                        While reader.Read()
                            DataGridView1.Rows.Add(
                                reader("sales_id"),
                                reader("sales_name"),
                                reader("sales_dosage"),
                                reader("sales_qty"),
                                reader("sales_price")
                            )

                            totalSalesPrice += Convert.ToDecimal(reader("sales_price"))
                        End While

                        ' Display the total sales price in Label8
                        Label8.Text = $"₱{totalSalesPrice:N2}"
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error retrieving compute_sales data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub SalesWindow_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Display Discountts
        ComboBox1.Items.Add("Regular Discount (0%)")
        ComboBox1.Items.Add("SENIOR / PWD DISCOUNT: 20%")
        ComboBox1.Items.Add("SENIOR / PWD WITH BOOK AND PRESCRIPTION: 32%")
        ComboBox1.SelectedIndex = 0

        If DataGridView1.Columns.Count = 0 Then
            DataGridView1.Columns.Add("SalesId", "Sales ID")
            DataGridView1.Columns.Add("SalesName", "Item Name")
            DataGridView1.Columns.Add("SalesDosage", "Item Dosage")
            DataGridView1.Columns.Add("ColumnSalesQty", "Item Quantity")
            DataGridView1.Columns.Add("ColumnSalesPrice", "Item Price")
        End If

        DisplayComputeSalesData()

        If Decimal.TryParse(Label8.Text.Replace("₱", "").Replace(",", "").Trim(), totalAmount) Then
            'shit
        Else
            MessageBox.Show("Invalid total amount format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        ' Get the selected item from ComboBox1
        Dim selectedDiscountOption As String = ComboBox1.SelectedItem.ToString()

        ' Call ApplyDiscount based on the selected option
        If selectedDiscountOption = "SENIOR / PWD DISCOUNT: 20%" Then
            ApplyDiscount(0.2)
        ElseIf selectedDiscountOption = "SENIOR / PWD WITH BOOK AND PRESCRIPTION: 32%" Then
            ApplyDiscount(0.32)
        ElseIf selectedDiscountOption = "Regular Discount (0%)" Then
            ApplyDiscount(0)
        End If
    End Sub

    Private Sub ApplyDiscount(discount As Double)
        Dim discountedAmount As Decimal = totalAmount * (1 - CDec(discount))
        Label5.Text = $"₱{discountedAmount:N2}"
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        If Not Decimal.TryParse(TextBox1.Text, Nothing) AndAlso TextBox1.Text <> "." Then
            TextBox1.Clear()
            MessageBox.Show("Please enter a valid numeric value.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
        UpdateChangeLabel()
    End Sub

    Private Sub UpdateChangeLabel()
        If Decimal.TryParse(TextBox1.Text, Nothing) AndAlso Decimal.TryParse(Label8.Text.Replace("₱", "").Replace(",", "").Trim(), totalAmount) Then
            Dim discount As Double = 0
            Select Case ComboBox1.SelectedItem.ToString()
                Case "SENIOR / PWD DISCOUNT: 20%"
                    discount = 0.2
                Case "SENIOR / PWD WITH BOOK AND PRESCRIPTION: 32%"
                    discount = 0.32
                Case "Regular Discount (0%)"
                    discount = 0
            End Select

            Dim discountedAmount As Decimal = totalAmount * (1 - CDec(discount))
            Dim cashValue As Decimal = Decimal.Parse(TextBox1.Text)
            Dim result As Decimal = cashValue - discountedAmount

            If result < 0 Then
                result = 0
            End If

            Label6.Text = $"₱{result:N2}"
        Else
            Label6.Text = "Insufficient"
        End If
    End Sub

    Private Sub DeleteRowFromDatabase(salesId As Integer)
        Try
            Using connection As New MySqlConnection(connectionString)
                connection.Open()

                ' Retrieve the sales information before deleting
                Dim salesQuantity As Integer = RetrieveSalesQuantity(salesId)
                Dim itemName As String = RetrieveSalesItemName(salesId)

                ' Delete the row from compute_sales table
                Dim deleteQuery As String = "DELETE FROM compute_sales WHERE sales_id = @salesId"

                Using deleteCmd As New MySqlCommand(deleteQuery, connection)
                    deleteCmd.Parameters.AddWithValue("@salesId", salesId)
                    deleteCmd.ExecuteNonQuery()
                End Using

                ' Update the inventory table with the sales information
                Dim updateQuery As String = "UPDATE inventory SET item_qty = item_qty + @salesQuantity WHERE item_name = @itemName"

                Using updateCmd As New MySqlCommand(updateQuery, connection)
                    updateCmd.Parameters.AddWithValue("@salesQuantity", salesQuantity)
                    updateCmd.Parameters.AddWithValue("@itemName", itemName)
                    updateCmd.ExecuteNonQuery()
                End Using

                MessageBox.Show("Medicine deleted and retrieved the item quantity to inventory successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Using
        Catch ex As Exception
            MessageBox.Show("Error deleting row from compute_sales: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function RetrieveSalesQuantity(salesId As Integer) As Integer
        Dim salesQuantity As Integer = 0

        Try
            Using connection As New MySqlConnection(connectionString)
                connection.Open()

                Dim query As String = "SELECT sales_qty FROM compute_sales WHERE sales_id = @salesId"

                Using cmd As New MySqlCommand(query, connection)
                    cmd.Parameters.AddWithValue("@salesId", salesId)

                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            salesQuantity = Convert.ToInt32(reader("sales_qty"))
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error retrieving sales quantity: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return salesQuantity
    End Function

    Private Function RetrieveSalesItemName(salesId As Integer) As String
        Dim itemName As String = String.Empty

        Try
            Using connection As New MySqlConnection(connectionString)
                connection.Open()

                Dim query As String = "SELECT sales_name FROM compute_sales WHERE sales_id = @salesId"

                Using cmd As New MySqlCommand(query, connection)
                    cmd.Parameters.AddWithValue("@salesId", salesId)

                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            itemName = Convert.ToString(reader("sales_name"))
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error retrieving sales item name: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        Return itemName
    End Function

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        ' Get the selected row from DataGridView
        If DataGridView1.SelectedRows.Count > 0 Then
            Dim selectedRowIndex As Integer = DataGridView1.SelectedRows(0).Index
            Dim salesId As Integer = Convert.ToInt32(DataGridView1.Rows(selectedRowIndex).Cells("SalesId").Value)
            ' Call the method to delete the selected row from the database
            DeleteRowFromDatabase(salesId)
            'automatic refresh please
            RefreshDataGridView()
        Else
            MessageBox.Show("Please select a row to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub RefreshDataGridView()
        Try
            DisplayComputeSalesData()
        Catch ex As Exception
            MessageBox.Show("Error refreshing DataGridView: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
