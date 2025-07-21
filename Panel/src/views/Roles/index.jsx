import React from 'react';
import Box from '@mui/material/Box';
import { DataGrid } from '@mui/x-data-grid';

const columns = [
    { field: 'id', headerName: 'Identity', width: 300 },
    { field: 'roleName', headerName: 'Role Name', width: 300 },
    { field: 'roleLevel', headerName: 'Role Level', width: 100 }
];

export default function RolesPage() {
    const [rows, setRows] = React.useState([]);
    const [loading, setLoading] = React.useState(true);

    React.useEffect(() => {
        const fetchRoles = async () => {
            try {
                const response = await fetch("https://localhost:7095/api/Role");
                if (!response.ok) {
                    throw new Error('Network response was not ok');
                }
                const json = await response.json();
                setRows(json.data);
            } catch (error) {
                console.error('Fetching error:', error);
            } finally {
                setLoading(false);
            }
        };

        fetchRoles();
    }, []);

    return (
        <Box sx={{ height: 400, width: '100%' }}>
            <DataGrid
                rows={rows}
                columns={columns}
                getRowId={(row) => row.id}
                loading={loading}
                initialState={{
                    pagination: {
                        paginationModel: { pageSize: 5 }
                    }
                }}
                pageSizeOptions={[5]}
                checkboxSelection
                disableRowSelectionOnClick
            />
        </Box>
    );
}
