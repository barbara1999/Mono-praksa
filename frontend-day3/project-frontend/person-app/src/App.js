import React, { Component } from "react";
import {
  Table,
  Button,
  Modal,
  ModalHeader,
  ModalBody,
  ModalFooter,
  FormGroup,
  Label,
  Input,
} from "reactstrap";
import axios from "axios";

class App extends Component {
  state = {
    persons: [],
    newPersonModal: false,
    newPersonData: {
      id: "",
      name: "",
      surname: "",
      city: "",
    },
    editPersonData: {
      id: "",
      name: "",
      surname: "",
      city: "",
    },
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
      newPersonModal: !this.state.newPersonModal,
    });
    //this.state.newPersonModal = true;
  }

  addPerson() {
    axios
      .post("https://localhost:44359/api/person", this.state.newPersonData)
      .then((response) => {
        let { persons } = this.state;
        persons.push(response.data);
        this.setState({
          persons,
          newPersonModal: false,
          newPersonData: {
            id: "",
            name: "",
            surname: "",
            city: "",
          },
        });
      });
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
            <Button
              color="success"
              size="sm"
              onClick={this.editPerson.bind(
                this,
                person.id,
                person.name,
                person.surname,
                person.city
              )}
            >
              Edit
            </Button>
            <Button color="danger" size="sm" className="pl-2">
              Delete
            </Button>
          </td>
        </tr>
      );
    });

    return (
      <div className="App container">
        <h1>Persons App</h1>
        <Button
          className="my-3"
          color="primary"
          onClick={this.toggleNewPersonModal.bind(this)}
        >
          Add Person
        </Button>
        <Modal
          isOpen={this.state.newPersonModal}
          toggle={this.toggleNewPersonModal.bind(this)}
        >
          <ModalHeader toggle={this.toggleNewPersonModal.bind(this)}>
            Add new person
          </ModalHeader>
          <ModalBody>
            <FormGroup>
              <Label for="id">Id</Label>
              <Input
                type="number"
                id="id"
                value={this.state.newPersonData.id}
                onChange={(e) => {
                  let { newPersonData } = this.state;
                  newPersonData.id = e.target.value;
                  this.setState({ newPersonData });
                }}
              />
            </FormGroup>
            <FormGroup>
              <Label for="name">Name</Label>
              <Input
                type="text"
                id="name"
                value={this.state.newPersonData.name}
                onChange={(e) => {
                  let { newPersonData } = this.state;
                  newPersonData.name = e.target.value;
                  this.setState({ newPersonData });
                }}
              />
            </FormGroup>
            <FormGroup>
              <Label for="surname">Surname</Label>
              <Input
                type="text"
                id="surname"
                value={this.state.newPersonData.surname}
                onChange={(e) => {
                  let { newPersonData } = this.state;
                  newPersonData.surname = e.target.value;
                  this.setState({ newPersonData });
                }}
              />
            </FormGroup>
            <FormGroup>
              <Label for="city">Post Number</Label>
              <Input
                type="number"
                id="city"
                value={this.state.newPersonData.city}
                onChange={(e) => {
                  let { newPersonData } = this.state;
                  newPersonData.city = e.target.value;
                  this.setState({ newPersonData });
                }}
              />
            </FormGroup>
          </ModalBody>
          <ModalFooter>
            <Button color="primary" onClick={this.addPerson.bind(this)}>
              Add person
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
