import React, { Component } from "react";
import { Modal, Button, Row, Col, Form, FormGroup } from "react-bootstrap";

import SnackBar from "@material-ui/core/SnackBar";
import IconButton from "@material-ui/core/IconButton";

export class EditEmpModal extends Component {
  constructor(props) {
    super(props);

    this.state = { deps: [], snackbaropen: false, snackbarmsg: "" };
    this.handleSubmit = this.handleSubmit.bind(this);
  }

  componentDidMount() {
    fetch("https://localhost:44393/api/department")
      .then((response) => response.json())
      .then((data) => {
        this.setState({ deps: data });
      });
  }

  snackbarClose = (event) => {
    this.setState({ snackbaropen: false });
  };

  handleSubmit(event) {
    event.preventDefault();
    fetch("https://localhost:44393/api/employee", {
      method: "PUT",
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        EmployeeID: event.target.EmployeeID.value,
        EmployeeName: event.target.EmployeeName.value,
        Department: event.target.Department.value,
        MailID: event.target.MailID.value,
        DOJ: event.target.DOJ.value,
      }),
    })
      .then((res) => res.json())
      .then(
        (result) => {
          this.setState({ snackbaropen: true, snackbarmsg: result });
          //alert(result);
        },
        (error) => {
          this.setState({ snackbaropen: true, snackbarmsg: "Failed" });
          //alert("Failed");
        }
      );
    // alert(event.target.DepartmentName.value);
  }

  render() {
    return (
      <div className="container">
        <SnackBar
          anchorOrigin={{ vertical: "center", horizontal: "center" }}
          open={this.state.snackbaropen}
          autoHideDuration={3000}
          onClose={this.snackbarClose}
          message={<span id="message-id"> {this.state.snackbarmsg} </span>}
          action={[
            <IconButton
              key="close"
              arial-label="Close"
              color="inherit"
              onClick={this.snackbarClose}
            >
              x
            </IconButton>,
          ]}
        />
        <Modal
          {...this.props}
          size="lg"
          aria-labelledby="contained-modal-title-vcenter"
          centered
        >
          <Modal.Header closeButton>
            <Modal.Title id="contained-modal-title-vcenter">
              Edit Employee
            </Modal.Title>
          </Modal.Header>
          <Modal.Body>
            <Row>
              <Col sm={6}>
                <Form onSubmit={this.handleSubmit}>
                  <Form.Group controlId="EmployeeID">
                    <Form.Label>Employee ID</Form.Label>
                    <Form.Control
                      type="text"
                      name="EmployeeID"
                      disabled
                      defaultValue={this.props.empid}
                      placeholder="Employee Name"
                    />
                  </Form.Group>

                  <Form.Group controlId="EmployeeName">
                    <Form.Label>Employee Name</Form.Label>
                    <Form.Control
                      type="text"
                      name="EmployeeName"
                      required
                      defaultValue={this.props.empname}
                      placeholder="Employee Name"
                    />
                  </Form.Group>

                  <Form.Group controlId="Department">
                    <Form.Label>Department</Form.Label>
                    <Form.Control as="select" defaultValue={this.props.depmt}>
                      {this.state.deps.map((dep) => (
                        <option key={dep.DepartmentID}>
                          {dep.DepartmentName}
                        </option>
                      ))}
                    </Form.Control>
                  </Form.Group>

                  <Form.Group controlId="MailID">
                    <Form.Label>MailID</Form.Label>
                    <Form.Control
                      type="text"
                      name="MailID"
                      required
                      defaultValue={this.props.mailid}
                      placeholder="MailID"
                    />
                  </Form.Group>

                  <Form.Group controlId="DOJ">
                    <Form.Label>DOJ</Form.Label>
                    <Form.Control
                      type="date"
                      name="DOJ"
                      required
                      defaultValue={this.props.doj}
                      placeholder="DOJ"
                    />
                  </Form.Group>

                  <Form.Group>
                    <Button variant="primary" type="submit">
                      Update Employee
                    </Button>
                  </Form.Group>
                </Form>
              </Col>
            </Row>
          </Modal.Body>
          <Modal.Footer>
            <Button variant="danger" onClick={this.props.onHide}>
              Close
            </Button>
          </Modal.Footer>
        </Modal>
      </div>
    );
  }
}
