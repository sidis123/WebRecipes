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
const AllCategories = ({ needRefetch }) => {
  const navigate = useNavigate() // React Router navigation hook
  const [categories, setCategories] = useState([])
  const token = localStorage.getItem('token')

  useEffect(() => {
    fetchCategories()
  }, [])

  useEffect(() => {
    fetchCategories()
  }, [needRefetch])

  const fetchCategories = () => {
    axios
      .get('https://localhost:7120/api/Category', {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      })
      .then((response) => {
        setCategories(response.data)
      })
  }

  const RedirectToEdit = (categoryId) => {
    navigate(`/edit-category/${categoryId}`)
  }
  const deleteCategory = (categoryId) => {
    axios
      .delete(`https://localhost:7120/api/Category/${categoryId}`, {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      })
      .then((response) => {
        alert('Category deleted successfully!')
        fetchCategories()
      })
  }

  return (
    <div style={{ overflowX: 'auto' }}>
      <CTable striped hover responsive>
        <CTableHead>
          <CTableRow>
            <CTableHeaderCell scope="col">Category ID</CTableHeaderCell>
            <CTableHeaderCell scope="col">Category Name</CTableHeaderCell>
            <CTableHeaderCell scope="col">Actions</CTableHeaderCell>
          </CTableRow>
        </CTableHead>
        <CTableBody>
          {categories.map((category) => (
            <CTableRow key={category.id_Kategorija}>
              <CTableDataCell>{category.id_Kategorija}</CTableDataCell>
              <CTableDataCell>{category.pavadinimas}</CTableDataCell>
              <CTableDataCell>
                <div className="d-flex gap-4">
                  <CButton onClick={() => RedirectToEdit(category.id_Kategorija)} color="primary">
                    Edit
                  </CButton>
                  <CButton onClick={() => deleteCategory(category.id_Kategorija)} color="danger">
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
export default AllCategories
