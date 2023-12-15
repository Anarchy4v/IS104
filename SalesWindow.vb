Imports System.Security.Cryptography.X509Certificates
Imports MySql.Data.MySqlClient

Public Module SharedVariables
    Public totalSalesPrice As Decimal
    Public cashValue As Decimal
    Public Result As Decimal
    Public vatAmount As Decimal
    Public discount As Decimal
    Public totalQty As Decimal
End Module

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

    Private Sub DisplayComputeSalesData()
        Try
            Using connection As New MySqlConnection(connectionString)
                connection.Open()

                Dim query As String = "SELECT sales_id, sales_name, sales_dosage, sales_qty, sales_price FROM compute_sales"

                Using cmd As New MySqlCommand(query, connection)
                    Using reader As MySqlDataReader = cmd.ExecuteReader()
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
                            totalQty += Convert.ToInt32(reader("sales_qty"))
                        End While

                        Dim vatPercentage As Decimal
                        If totalQty <= 10 Then
                            vatPercentage = 0.04 '
                        ElseIf totalQty <= 20 Then
                            vatPercentage = 0.08 '
                        Else
                            vatPercentage = 0.12 '
                        End If

                        Dim vatAmount As Decimal = totalSalesPrice * vatPercentage
                        Label8.Text = $"₱{totalSalesPrice:N2}"
                        Label10.Text = $"VAT: ₱{vatAmount:N2} (QTY: {totalQty})"
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
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Save relevant data to SharedVariables
        SharedVariables.totalSalesPrice = totalSalesPrice
        SharedVariables.cashValue = Decimal.Parse(TextBox1.Text)
        SharedVariables.Result = Decimal.Parse(Label6.Text.Replace("₱", "").Replace(",", "").Trim())
        SharedVariables.vatAmount = Decimal.Parse(Label10.Text.Replace("VAT: ₱", "").Replace(",", "").Split(" "c)(0))
        SharedVariables.discount = GetDiscountValue()

        ' Ask for confirmation before generating the receipt
        Dim confirmationResult As DialogResult = MessageBox.Show("Do you want to generate the receipt? This cannot be undone", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If confirmationResult = DialogResult.Yes Then
            ' Generate the receipt
            InsertIntoSalesReport()

            ' Open CustomerGeneratedReport form
            Dim customerReportForm As New CustomerGeneratedReport()
            customerReportForm.Show()
        End If
    End Sub

    Private Function GetDiscountValue() As Decimal
        Dim discountValue As Decimal = 0

        Select Case ComboBox1.SelectedItem.ToString()
            Case "SENIOR / PWD DISCOUNT: 20%"
                discountValue = 0.2
            Case "SENIOR / PWD WITH BOOK AND PRESCRIPTION: 32%"
                discountValue = 0.32
            Case "Regular Discount (0%)"
                discountValue = 0
        End Select

        Return discountValue
    End Function

    Private Sub InsertIntoSalesReport()
        Try
            Using connection As New MySqlConnection(connectionString)
                connection.Open()

                ' Insert query
                Dim insertQuery As String = "INSERT INTO salesreport (order_id, user_id, order_qty, total_price, discount) VALUES (@orderId, @userId, @orderQty, @totalPrice, @discount)"

                ' Parameters for the insert query
                Dim parameters As New List(Of MySqlParameter)
                parameters.Add(New MySqlParameter("@orderId", DBNull.Value)) ' Auto-incremented ID, so we don't need to provide a value
                parameters.Add(New MySqlParameter("@userId", 1)) ' Replace 1 with the actual user ID
                parameters.Add(New MySqlParameter("@orderQty", totalQty)) ' Use the provided orderQty parameter
                parameters.Add(New MySqlParameter("@totalPrice", cashValue)) ' Use the retrieved totalSalesPrice
                parameters.Add(New MySqlParameter("@discount", GetDiscountValue())) ' Use the retrieved discount value

                Using cmd As New MySqlCommand(insertQuery, connection)
                    cmd.Parameters.AddRange(parameters.ToArray())
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            MessageBox.Show("Error inserting data into salesreport table: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class
