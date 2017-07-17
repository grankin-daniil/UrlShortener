import React from "react";

module.exports = React.createClass({
    render: function() {
        return (
            <strong>{this.props.text}</strong>
        )
    }
});