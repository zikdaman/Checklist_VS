var attributesDiv = document.getElementById('attributesDiv');
Sortable.create(attributesDiv, {
    onEnd: function () {
        setAttributeOrder();
    }
});