
    $(document).ready(function () {
        $('#Birth').datepicker({
            dateFormat: "dd/mm/yy",
            showStatus: true,
            showWeeks: true,
            currentText: 'Now',
            autoSize: true,
            gotoCurrent: true,
            showAnim: 'blind',
            highlightWeek: true
        });
    });
