import React from "react";
import ReactDOM from "react-dom";
import axios from "axios";

import InputButtonGroup from "./react_components/inputButtonGroup.jsx";
import Output from "./react_components/output.jsx";

const baseUrl = 'http://localhost:63597/';

const App = React.createClass({
    getInitialState() {
        return {
            sending: false,
        }
    },

    isUrl(str) {
        var urlRegex = /^(https?):\/\/([a-zA-Z0-9.-]+(:[a-zA-Z0-9.&%$-]+)*@)*((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[1-9][0-9]?)(\.(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[1-9]?[0-9])){3}|([a-zA-Z0-9-]+\.)*[a-zA-Z0-9-]+\.(com|edu|gov|int|mil|net|org|biz|arpa|info|name|pro|aero|coop|museum|[a-zA-Z]{2}))(:[0-9]+)*(\/($|[a-zA-Z0-9.,?'\\+&%$#=~_-]+))*$/;
        return urlRegex.test(str);
    },

    shortenUrl(url) {
        this.setState({
            sending: true,
            shortenedUrl: ''
        });

        axios
            .post(`${baseUrl}/api/shorten`, {
                url: url,
            })
            .then((res) => { 
                this.setState({ sending: false, shortenedUrl: res.data}) 
            })
            .catch(() => {
                alert('Something went wrong :(');
                this.setState({ sending: false, shortenedUrl: ''})
            });
    },

    render() {
        return (
            <div>
                 <InputButtonGroup text="Your original URL here" onBtnClick ={this.shortenUrl} isValid={this.isUrl}
                    iconClass="glyphicon glyphicon-scissors" btnText="Shorten" />
                 <hr />
                 <div className="center-block">
                    { this.state.sending ? <span className="glyphicon glyphicon-send"></span> : null }
                    <Output text={this.state.shortenedUrl} />
                </div>
            </div>
        );
}
});

ReactDOM.render(<App />, document.getElementById("app"));