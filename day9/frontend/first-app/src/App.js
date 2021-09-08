import logo from "./logo.svg";
import "./App.css";
import React, { Component } from "react";
import Form from "./Form.js";

function Welcome(props) {
  return <h1>Hello, {props.name}</h1>;
}

class Welcome2 extends React.Component {
  render() {
    return <h1>Hello again, {this.props.name}</h1>;
  }
}

function App() {
  return (
    <div className="App">
      <Form></Form>
    </div>
  );
}

export default App;
