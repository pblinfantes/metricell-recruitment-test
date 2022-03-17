import React, { useState } from 'react';
import { Table, Row, Col, Container, Button, Modal, Form } from 'react-bootstrap';

const defaultEmployee = {
    name: "",
    value: 0
};

function EmployeeList(props) {
    const { employees, addEmployeeRequest, updateEmployeeRequest, removeEmployeeRequest } = props;
    const [showModal, setShowModal] = useState({ show: false, insertMode: false });
    const [selectedEmployee, setSelectedEmployee] = useState(defaultEmployee);
    const [updatedEmployee, setUpdatedEmployee] = useState(defaultEmployee);

    const showUpdateModal = (employee) => {
        setShowModal({
            show: true,
            insertMode:false
        });
        setSelectedEmployee(employee);
        setUpdatedEmployee(employee);
    }

    const showCreateModal = () => {
        setShowModal({
            show: true,
            insertMode: true
        });
        setSelectedEmployee(defaultEmployee);
    }

    const handleHideModal = () => {
        setShowModal(false);
        setSelectedEmployee(defaultEmployee);
        setUpdatedEmployee(defaultEmployee);
    }

    const handleInputChangeUpdateMode = event => {
        const { name, value } = event.target;
        setUpdatedEmployee({
            ...updatedEmployee,
            [name] : value
        });
    }

    const handleInputChangeCreateMode = event => {
        const { name, value } = event.target;
        setUpdatedEmployee({
            ...updatedEmployee,
            [name]: value
        });
    }


    const addEmployee = () => {
        addEmployeeRequest(updatedEmployee);
        handleHideModal();
    }

    

    const updateEmployee = () => {
        handleHideModal();
        updateEmployeeRequest(selectedEmployee, updatedEmployee);
    }

    

    const removeEmployee = (employee) => {
        removeEmployeeRequest(employee);
    }

    return (
        <>
            <Col md={4} className="separator">
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

            <Modal show={showModal.show}>
                <Modal.Header>
                    <Modal.Title> {showModal.insertMode ? "Create new employee" : "Update employee"} </Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form.Group>
                        <Form.Label> Name </Form.Label>
                        <Form.Control type="text" name="name" defaultValue={selectedEmployee.name} onChange={showModal.insertMode ? handleInputChangeCreateMode : handleInputChangeUpdateMode} />
                    </Form.Group>
                    <Form.Group>
                        <Form.Label> Value </Form.Label>
                        <Form.Control type="number" name="value" defaultValue={selectedEmployee.value} onChange={showModal.insertMode ? handleInputChangeCreateMode : handleInputChangeUpdateMode} />
                    </Form.Group>
                </Modal.Body>
                <Modal.Footer>
                    <Button className="m-1" variant="success" onClick={() => showModal.insertMode ? addEmployee() : updateEmployee()}> Save </Button>
                    <Button className="m-1" variant="secondary" onClick={handleHideModal}> Cancel </Button>
                </Modal.Footer>
            </Modal>
        </>

    )
}

export default EmployeeList;