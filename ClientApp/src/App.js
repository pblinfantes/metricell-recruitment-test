import React, { useState, useEffect, Component } from 'react';
import { Container, Row, Col } from 'react-bootstrap';
import axios from 'axios';
import EmployeeList from './EmployeeList';
import EmployeeQueries from './EmployeeQueries'

function App() {
    const baseUrl = "http://localhost:41478/employees";
    const baseUrlList = "http://localhost:41478/list/";
    const [employees, setEmployees] = useState([]);
    const [incrementedEmployees, setIncrementedEmployees] = useState([]);
    const [employeesValueSum, setEmployeesValueSum] = useState(null);

    const updateAllEmployeesDatasources = () => {
        getEmployeesRequest();
        getIncrementedEmployeesRequest();
        getEmployeesValueSumRequest();
    }

    const getEmployeesRequest = async () => {
        await axios.get(baseUrl).then(response => {
            setEmployees(response.data);
        }).catch(error => {
            alert(`Error querying employees: ${error}`);
        })
    };

    const getIncrementedEmployeesRequest = async () => {
        await axios.get(baseUrlList + "getEmployeesWithIncrementedValue").then(response => {
            setIncrementedEmployees(response.data);
        }).catch(error => {
            alert(`Error querying incremented employees: ${error}`);
        })
    };

    const getEmployeesValueSumRequest = async () => {
        await axios.get(baseUrlList + "GetEmployeesValueSum").then(response => {
            setEmployeesValueSum(response.data);
        }).catch(error => {
            alert(`Error querying employees value sum: ${error}`);
        })
    };

    const addEmployeeRequest = async (employee) => {
        console.log(employee);
        await axios.post(baseUrl, employee).then(response => {
            let data = response.data;
            if (data.success)
                updateAllEmployeesDatasources();
        }).catch(error => {
            alert(`Error adding employee: ${error}`);
        });
    };

    const updateEmployeeRequest = async (originalEmployee, updatedEmployee) => {
        let updateRequest = {
            originalEmployee: originalEmployee,
            updatedEmployee: updatedEmployee
        };
        await axios.put(baseUrl, updateRequest).then(response => {
            updateAllEmployeesDatasources();
        }).catch(error => {
            alert(`Error updating employee: ${error}`);
        });
    };

    const removeEmployeeRequest = async (employee) => {
        await axios.delete(baseUrl, { data: employee }).then(response => {
            updateAllEmployeesDatasources();
        }).catch(error => {
            alert(`Error removing employee: ${error}`);
        });
    };

    useEffect(() => {
        getEmployeesRequest();
        getIncrementedEmployeesRequest();
        getEmployeesValueSumRequest();
    }, []);

    return (
        <Container>
            <Row>
                <EmployeeList
                    employees={employees}
                    addEmployeeRequest={addEmployeeRequest}
                    updateEmployeeRequest={updateEmployeeRequest}
                    removeEmployeeRequest={removeEmployeeRequest}
                />

                <EmployeeQueries
                    incrementedEmployees={incrementedEmployees} />

                <Col md={4} className="separator">
                    {employeesValueSum >= 11171 ? <h2> Employees value sum: { employeesValueSum} </h2> : <h2>Employees value sum is less than 11171</h2> }
                </Col>
            </Row>
        </Container>
    )
}

export default App;