import React, { useState, useEffect, Component } from 'react';
import axios from 'axios';

function EmployeeList() {
    const baseUrl = "http://localhost:41478/employees";
    const [employees, setEmployees] = useState([]);

    const getEmployees = async () => {
        await axios.get(baseUrl).then(response => {
            setEmployees(response.data);
        }).catch(error => {
            alert(`Error querying employees: ${error}`);
        })
    };

    useEffect(() => {
        getEmployees();
    }, []);

    return (
        <div>
            <table>
                <thead>
                    <tr>
                        <th> Name </th>
                        <th> Value </th>
                    </tr>
                </thead>
                <tbody>
                    {employees.map(employee => (
                        <tr>
                            <td>{employee.name}</td>
                            <td>{employee.value}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    )
}

export default EmployeeList;