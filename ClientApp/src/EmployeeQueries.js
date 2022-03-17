import React from 'react';
import { Table, Row, Col, Container, Button, Modal, Form } from 'react-bootstrap';


function EmployeeQueries(props) {
    const { incrementedEmployees } = props;

    return (
        <Col md={4} className="separator">
            <span style={{fontSize:"26px"}}> Employees with incremented Values </span>
            <Table striped bordered hover size="sm">
                <thead>
                    <tr>
                        <th> Name </th>
                        <th> Value </th>
                    </tr>
                </thead>
                <tbody>
                    {incrementedEmployees.map(incrementedEmployee => (
                        <tr key={incrementedEmployee.name} style={{ height: "48.5px" }}>
                            <td>{incrementedEmployee.name}</td>
                            <td>{incrementedEmployee.value}</td>
                        </tr>
                    ))}
                </tbody>
            </Table>
        </Col>
    )
}

export default EmployeeQueries;