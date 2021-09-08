import React, { Component } from "react";
import "./Form.css";
class Form extends Component {
  constructor(props) {
    super(props);

    this.state = {
      username: "",
      comments: "",
      topic: "react",
    };
  }

  handleUserNameChange = (event) => {
    this.setState({
      username: event.target.value,
    });
  };

  handleCommentsChange = (event) => {
    this.setState({
      comments: event.target.value,
    });
  };

  handleTopic = (event) => {
    this.setState({
      topic: event.target.value,
    });
  };

  handleSubmit = (event) => {
    alert(`${this.state.username} ${this.state.comments} ${this.state.topic}`);
    event.preventDefault();
  };

  render() {
    const { username, comments, topic } = this.state;
    return (
      <form onSubmit={this.handleSubmit} class="form-container">
        <div class="item">
          <label htmlFor="">Username</label>
          <input
            type="text"
            value={username}
            onChange={this.handleUserNameChange}
          />
        </div>
        <div class="item">
          <label htmlFor="">Comments</label>
          <textarea
            value={comments}
            onChange={this.handleCommentsChange}
            name=""
            id=""
            cols="18"
            rows="3"
          ></textarea>
        </div>
        <div class="item">
          <label htmlFor="">Topic</label>
          <select value={topic} onChange={this.handleTopic} name="" id="">
            <option value="react">React</option>
            <option value="angular">Angular</option>
            <option value="vue">Vue</option>
          </select>
        </div>
        <button class="btn-submit" type="submit">
          Submit
        </button>
      </form>
    );
  }
}

export default Form;
