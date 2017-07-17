import React from "react";

module.exports = React.createClass({
    getInitialState() {
        return {
            value: '',
            disabled: true
        };
    },
    handleChange(e) {
        var value = e.target.value;
        var disabled = !value;
        if (typeof this.props.isValid === "function") {
            disabled = !this.props.isValid(value);
        }

        this.setState({
            value: value,
            disabled: disabled
        });
    },
    render: function() {
        return (
            <div className="input-group">
                <input className="form-control" type="text" value={this.state.value} onChange={this.handleChange} placeholder={this.props.text} />
                <span className="input-group-btn">
                    <button className="btn btn-secondary" type="button" disabled={this.state.disabled} 
                        onClick={(e) => this.props.onBtnClick(this.state.value)}>
                        <span className={this.props.iconClass}></span> {this.props.btnText}
                    </button>
                </span>
            </div>
        )
    }
});