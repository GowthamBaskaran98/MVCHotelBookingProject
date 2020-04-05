$(document).ready(function () {
    $('#dateTo').datepicker({
        dateFormat: 'd MM yy'
    });

    $('#dateFrom').datepicker({
        dateFormat: 'd MM yy',
        onSelect: function () {
            //Get the selected data
            var selectedDate = $(this).datepicker('getDate');
            // Add 10 days to it
            selectedDate.setDate(selectedDate.getDate() + 10);
            // Set the DateTo Date
            $('#dateTo').datepicker('setDate', selectedDate);
        }
    });
});