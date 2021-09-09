import React, { Component } from "react";
import {
  Table,
  Button,
  Modal,
  ModalHeader,
  ModalBody,
  ModalFooter,
} from "reactstrap";
import axios from "axios";

class App extends Component {
  state = {
    persons: [],
    newPersonModal: false,
  };

  componentWillMount() {
    axios.get("https://localhost:44359/api/person").then((response) => {
      this.setState({
        persons: response.data,
      });
    });
  }

  toggleNewPersonModal() {
    this.setState({
      newPersonModal: true,
    });
    //this.state.newPersonModal = true;
  }

  render() {
    let persons = this.state.persons.map((person) => {
      return (
        <tr key={person.Id}>
          <td>{person.Id}</td>
          <td>{person.Name}</td>
          <td>{person.Surname}</td>
          <td>{person.City}</td>
          <td>
            <Button color="success" size="sm" className="mr-2">
              Edit
            </Button>
            <Button color="danger" size="sm">
              Delete
            </Button>
          </td>
        </tr>
      );
    });

    return (
      <div className="App">
        <Button color="primary" onClick={this.toggleNewPersonModal.bind(this)}>
          Add Person
        </Button>
        <Modal
          isOpen={this.state.newPersonModal}
          toggle={this.toggleNewPersonModal.bind(this)}
        >
          <ModalHeader toggle={this.toggleNewPersonModal.bind(this)}>
            Modal title
          </ModalHeader>
          <ModalBody>
            Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do
            eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim
            ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut
            aliquip ex ea commodo consequat. Duis aute irure dolor in
            reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla
            pariatur. Excepteur sint occaecat cupidatat non proident, sunt in
            culpa qui officia deserunt mollit anim id est laborum.
          </ModalBody>
          <ModalFooter>
            <Button
              color="primary"
              onClick={this.toggleNewPersonModal.bind(this)}
            >
              Do Something
            </Button>{" "}
            <Button
              color="secondary"
              onClick={this.toggleNewPersonModal.bind(this)}
            >
              Cancel
            </Button>
          </ModalFooter>
        </Modal>
        <Table>
          <thead>
            <tr>
              <th>#</th>
              <th>Name</th>
              <th>Surname</th>
              <th>City</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>{persons}</tbody>
        </Table>
      </div>
    );
  }
}

export default App;
