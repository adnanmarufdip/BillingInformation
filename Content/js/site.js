"use strict";

var selectElement = $('#item');
var unitPriceInput = $('#unitprice');
var subtotal = 0;
var discount = 0;
var vat = 0.12;


// Collect data from the input fields
$('#addToBill').click(function () {

    if ($('#quantity').val() > 0 && $('#quantity').val() % 1 == 0) {

        var fieldsData = {
            'date': $('#date').val(),
            'time': $('#time').val(),
            'item': $('#item').val(),
            'unitprice': $('#unitprice').val(),
            'quantity': $('#quantity').val(),
            'amount': $('#amount').val()
        }

        // convert fields data to JSON data
        var jsonData = JSON.stringify(fieldsData);
        console.log(jsonData);
    }
});

// Inserting data automaticlly during page load
$(document).ready(function () {
    var today = new Date();
    var year = today.getFullYear();
    var month = (today.getMonth() + 1).toString().padStart(2, '0');
    var day = today.getDate().toString().padStart(2, '0');
    var dateStr = year + '-' + month + '-' + day;
    $('#date').val(dateStr);
});

// Inserting time automaticlly during page load
$(document).ready(function () {
    setInterval(function () {
        var now = new Date();
        var hours = now.getHours().toString().padStart(2, '0');
        var minutes = now.getMinutes().toString().padStart(2, '0');
        var timeString = hours + ':' + minutes;
        $('#time').val(timeString);
    }, 10);
});


// Creating option for select using retrieve data from the database (ProductName)
$(document).ready(function () {

    $.each(productData, function (index, product) {
        selectElement.append($("<option>").attr("value", product.ProductName).text(product.ProductName));
    });

    updateUnitPrice();
});

// Selecting option also insert unit price for the selected item
function updateUnitPrice() {
    var selectedProduct = productData.find(function (product) {
        return product.ProductName === selectElement.val();
    });

    if (selectedProduct) {
        unitPriceInput.val(selectedProduct.UnitPrice);
    }
}

selectElement.on("change", function () {
    updateUnitPrice();
    convertDecimal();
});

// Setting the value to decimal point (2) => 0.00
function convertDecimal() {
    $('#unitprice').val(parseFloat($('#unitprice').val()).toFixed(2));
}

$(document).ready(function () {
    convertDecimal();
});

// Calculating the amount based on unit price and inserting it on amount field
$('#quantity').on("input", function () {
    const quantity = $(this).val();
    const unitPrice = $('#unitprice').val();
    const amount = (quantity * unitPrice).toFixed(2);
    $('#amount').val(amount);
});

// Button click adds new row on the table with selected info
$('#addToBill').click(function (event) {

    event.preventDefault();

    if ($('#quantity').val() > 0 && $('#quantity').val() % 1 == 0) {

        const item = $('#item').val();
        const quantity = $('#quantity').val();
        const unitPrice = $('#unitprice').val();
        const amount = $('#amount').val();

        // Create a new row and add it to the table
        const newRow = $("<tr>").append(
            $("<td>").text(item),
            $("<td>").text(quantity),
            $("<td>").text(unitPrice),
            $("<td>").text(amount)
        );
        $('#billTable').append(newRow);

        // Add the amount to the subtotal
        subtotal += parseFloat(amount);
        $('#subtotal').val(subtotal.toFixed(2));
        updateGrandTotal();
        resetValues();
    }
});

function resetValues() {

    event.preventDefault();
    // Reset the form fields
    $('#amount').val("");
    $('#quantity').val("0");
}

$('#reset').click(function (event) {
    resetValues();
});

$(document).ready(function () {

    // Set initial values of discount and vat fields
    $('#discount').val("0%");
    $('#vat').val("12%");
    $('#quantity').val("0");

    discount = parseFloat($('#discount').val().replace("%", "")) / 100;
    vat = parseFloat($('#vat').val().replace("%", "")) / 100;

    // Update discount and vat variables when their values changes
    $('#discount, #vat').on("input", function () {
        discount = parseFloat($("#discount").val().replace("%", "")) / 100;
        vat = parseFloat($("#vat").val().replace("%", "")) / 100;
        updateGrandTotal();
    });
});

// Calculating the grand total value automaticlly
function updateGrandTotal() {
    var grandTotal = (subtotal - subtotal * discount) * (1 + vat);
    $('#grandtotal').val(grandTotal.toFixed(2));
}


