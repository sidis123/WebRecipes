import React from 'react'
import { useState, useEffect } from 'react'
import axios from 'axios'
import { useNavigate } from 'react-router-dom'
import {
  CTab,
  CTable,
  CTableHead,
  CTableRow,
  CTableBody,
  CTableHeaderCell,
  CTableDataCell,
  CButton,
} from '@coreui/react'
const AllUsers = ({ needRefetch }) => {
  const navigate = useNavigate() // React Router navigation hook
  const [users, setUsers] = useState([])
  const token = localStorage.getItem('token')

  useEffect(() => {
    fetchUsers()
  }, [])

  useEffect(() => {
    fetchUsers()
  }, [needRefetch])

  const fetchUsers = () => {
    axios
      .get('https://localhost:7120/api/User', {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      })
      .then((response) => {
        setUsers(response.data)
      })
  }

  const RedirectToEdit = (userioId) => {
    navigate(`/edit-user/${userioId}`)
  }
  const deleteUser = (userioId) => {
    axios
      .delete(`https://localhost:7120/api/User/${userioId}`, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      })
      .then((response) => {
        alert('User deleted successfully!')
        fetchUsers()
      })
  }

  return (
    <div style={{ overflowX: 'auto' }}>
      <CTable striped hover responsive>
        <CTableHead>
          <CTableRow>
            <CTableHeaderCell scope="col">User ID</CTableHeaderCell>
            <CTableHeaderCell scope="col">Vardas</CTableHeaderCell>
            <CTableHeaderCell scope="col">Pavarde</CTableHeaderCell>
            <CTableHeaderCell scope="col">Email</CTableHeaderCell>
            <CTableHeaderCell scope="col">Telefonas</CTableHeaderCell>
            <CTableHeaderCell scope="col">Role</CTableHeaderCell>
            <CTableHeaderCell scope="col">Actions</CTableHeaderCell>
          </CTableRow>
        </CTableHead>
        <CTableBody>
          {users.map((user) => (
            <CTableRow key={user.id_Vartotojas}>
              <CTableDataCell>{user.id_Vartotojas}</CTableDataCell>
              <CTableDataCell>{user.vardas}</CTableDataCell>
              <CTableDataCell>{user.pavarde}</CTableDataCell>
              <CTableDataCell>{user.email}</CTableDataCell>
              <CTableDataCell>{user.telefonas}</CTableDataCell>
              <CTableDataCell>{user.role}</CTableDataCell>
              <CTableDataCell>
                <div className="d-flex gap-4">
                  <CButton onClick={() => RedirectToEdit(user.id_Vartotojas)} color="primary">
                    Edit
                  </CButton>
                  <CButton onClick={() => deleteUser(user.id_Vartotojas)} color="danger">
                    Delete
                  </CButton>
                </div>
              </CTableDataCell>
            </CTableRow>
          ))}
        </CTableBody>
      </CTable>
    </div>
  )
}
export default AllUsers
