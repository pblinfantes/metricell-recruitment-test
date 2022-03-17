import React, { useState, useEffect, Component } from 'react';
import { Table, Row, Col, Container, Button, Modal, Form } from 'react-bootstrap';
import axios from 'axios';

const defaultEmployee = {
    name: "",
    value: 0
};

function EmployeeList() {
    const baseUrl = "http://localhost:41478/employees";
    const [employees, setEmployees] = useState([]);
    const [showModal, setShowModal] = useState({ show: false, insertMode: false });
    const [selectedEmployee, setSelectedEmployee] = useState(defaultEmployee);
    const [updatedEmployee, setUpdatedEmployee] = useState(defaultEmployee);


    const showUpdateModal = (employee) => {
        setShowModal({
            show: true,
            insertMode:false
        });
        setSelectedEmployee(employee);
    }

    const showCreateModal = () => {
        setShowModal({
            show: true,
            insertMode: true
        });
    }

    const handleHideModal = () => {
        setShowModal(false);
        setSelectedEmployee(defaultEmployee);
        setUpdatedEmployee(defaultEmployee);
    }

    const handleInputChange = event => {
        const { name, value } = event.target;
        setUpdatedEmployee({
            ...updatedEmployee,
            [name] : value
        });
    }


    const getEmployeesRequest = async () => {
        await axios.get(baseUrl).then(response => {
            setEmployees(response.data);
        }).catch(error => {
            alert(`Error querying employees: ${error}`);
        })
    };

    const addEmployee = () => {
        addEmployeeRequest(updatedEmployee);
        handleHideModal();
    }

    const addEmployeeRequest = async (employee) => {
        console.log(employee);
        await axios.post(baseUrl, employee).then(response => {
            let data = response.data;
            if (data.success)
                getEmployeesRequest();
        }).catch(error => {
            alert(`Error adding employee: ${error}`);
        });
    }

    const updateEmployee = () => {
        handleHideModal();
        updateEmployeeRequest(selectedEmployee, updatedEmployee);
    }

    const updateEmployeeRequest = async (originalEmployee, updatedEmployee) => {
        let updateRequest = {
            originalEmployee: originalEmployee,
            updatedEmployee: updatedEmployee
        };
        await axios.put(baseUrl, updateRequest).then(response => {
            getEmployeesRequest();
        }).catch(error => {
            alert(`Error removing employee: ${error}`);
        });
    }

    const removeEmployee = (employee) => {
        removeEmployeeRequest(employee);
    }

    const removeEmployeeRequest = async (employee) => {
        await axios.delete(baseUrl, { data: employee }).then(response => {
            getEmployeesRequest();
        }).catch(error => {
            alert(`Error removing employee: ${error}`);
        });
    }
   

    useEffect(() => {
        getEmployeesRequest();
    }, []);

    return (
        <div>
            <Container>
                <Row>
                    <Col md={4}>
                        <Button className="m-1" variant="success" size="sm" onClick={() => showCreateModal()}>Create new employee</Button>
                        <Table striped bordered hover size="sm">
                            <thead>
                                <tr>
                                    <th> Name </th>
                                    <th> Value </th>
                                    <th> Actions </th>
                                </tr>
                            </thead>
                            <tbody>
                                {employees.map(employee => (
                                    <tr key={employee.name}>
                                        <td>{employee.name}</td>
                                        <td>{employee.value}</td>
                                        <td>
                                            <Button className="m-1" variant="warning" size="sm" onClick={() => showUpdateModal(employee)}>Update</Button>
                                            <Button className="m-1" variant="danger" size="sm" onClick={() => removeEmployee(employee)}>Remove</Button>
                                        </td>
                                    </tr>
                                ))}
                            </tbody>
                        </Table>
                    </Col>
                </Row>
            </Container>

            <Modal show={showModal.show}>
                <Modal.Header>
                    <Modal.Title> {showModal.insertMode ? "Create new employee" : "Update employee"} </Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form.Group>
                        <Form.Label> Name </Form.Label>
                        <Form.Control type="text" name="name" defaultValue={selectedEmployee.name} onChange={handleInputChange} />
                    </Form.Group>
                    <Form.Group>
                        <Form.Label> Value </Form.Label>
                        <Form.Control type="number" name="value" defaultValue={selectedEmployee.value} onChange={handleInputChange} />
                    </Form.Group>
                </Modal.Body>
                <Modal.Footer>
                    <Button className="m-1" variant="success" onClick={() => showModal.insertMode ? addEmployee() : updateEmployee()}> Save </Button>
                    <Button className="m-1" variant="secondary" onClick={handleHideModal}> Cancel </Button>
                </Modal.Footer>
            </Modal>
        </div>

    )
}

export default EmployeeList;