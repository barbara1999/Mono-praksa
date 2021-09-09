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
    editPersonModal: false,
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
    this._refreshPerson();
  }

  toggleNewPersonModal() {
    this.setState({
      newPersonModal: !this.state.newPersonModal,
    });
    //this.state.newPersonModal = true;
  }

  toggleEditPersonModal() {
    this.setState({
      editPersonModal: !this.state.editPersonModal,
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
          editPersonModal: false,
          newPersonData: {
            id: "",
            name: "",
            surname: "",
            city: "",
          },
        });
      });
  }

  editPerson(id, name, surname, city) {
    this.setState({
      editPersonData: { id, name, surname, city },
      editPersonModal: !this.state.editPersonModal,
    });
  }

  updatePerson() {
    let { name, surname, city } = this.state.editPersonData;
    axios
      .put(
        "https://localhost:44359/api/person/" + this.state.editPersonData.id,
        {
          name,
          surname,
          city,
        }
      )
      .then((response) => {
        this._refreshPerson();
        this.setState({
          editPersonModal: false,
          editPersonData: { id: "", name: "", surname: "", city: "" },
        });
      });
  }

  _refreshPerson() {
    axios.get("https://localhost:44359/api/person").then((response) => {
      this.setState({
        persons: response.data,
      });
    });
  }

  deletePerson(id) {
    axios
      .delete("https://localhost:44359/api/person/" + id)
      .then((response) => {
        this._refreshPerson();
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
                person.Id,
                person.Name,
                person.Surname,
                person.City
              )}
            >
              Edit
            </Button>
            <Button
              color="danger"
              size="sm"
              className="pl-2"
              onClick={this.deletePerson.bind(this, person.Id)}
            >
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

        <Modal
          isOpen={this.state.editPersonModal}
          toggle={this.toggleEditPersonModal.bind(this)}
        >
          <ModalHeader toggle={this.toggleEditPersonModal.bind(this)}>
            Edit person
          </ModalHeader>
          <ModalBody>
            <FormGroup>
              <Label for="id">Id</Label>
              <Input
                type="number"
                id="id"
                value={this.state.editPersonData.id}
                onChange={(e) => {
                  let { editPersonData } = this.state;
                  editPersonData.id = e.target.value;
                  this.setState({ editPersonData });
                }}
              />
            </FormGroup>
            <FormGroup>
              <Label for="name">Name</Label>
              <Input
                type="text"
                id="name"
                value={this.state.editPersonData.name}
                onChange={(e) => {
                  let { editPersonData } = this.state;
                  editPersonData.name = e.target.value;
                  this.setState({ editPersonData });
                }}
              />
            </FormGroup>
            <FormGroup>
              <Label for="surname">Surname</Label>
              <Input
                type="text"
                id="surname"
                value={this.state.editPersonData.surname}
                onChange={(e) => {
                  let { editPersonData } = this.state;
                  editPersonData.surname = e.target.value;
                  this.setState({ editPersonData });
                }}
              />
            </FormGroup>
            <FormGroup>
              <Label for="city">Post Number</Label>
              <Input
                type="number"
                id="city"
                value={this.state.editPersonData.city}
                onChange={(e) => {
                  let { editPersonData } = this.state;
                  editPersonData.city = e.target.value;
                  this.setState({ editPersonData });
                }}
              />
            </FormGroup>
          </ModalBody>
          <ModalFooter>
            <Button color="primary" onClick={this.updatePerson.bind(this)}>
              Edit person
            </Button>{" "}
            <Button
              color="secondary"
              onClick={this.toggleEditPersonModal.bind(this)}
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
