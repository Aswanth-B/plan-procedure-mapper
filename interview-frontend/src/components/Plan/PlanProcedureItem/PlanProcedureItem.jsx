import React, { useEffect, useState } from "react";
import ReactSelect from "react-select";
import { addUserToPlanProcedure, removeUserFromPlanProcedure, removeAllUsersFromPlanProcedure } from "../../../api/api";
import {useParams} from 'react-router-dom';

const PlanProcedureItem = ({ procedure, users, planProcedureUsers }) => {
    const [selectedUsers, setSelectedUsers] = useState(null);
    const {id} = useParams();
    
    useEffect(() => {
        const existingUserIds = planProcedureUsers && planProcedureUsers.length > 0 ? planProcedureUsers.map(x => x.userId) : [];
        setSelectedUsers(users.filter(u => existingUserIds.includes(u.value)));
    }, [planProcedureUsers, users]);

    const handleAssignUserToProcedure = async (e, actionMeta) => {
        // TODO: Remove console.log and add missing logic
        switch(actionMeta.action){
            case "select-option":
                console.log(actionMeta.option.value);
                await addUserToPlanProcedure(parseInt(id), procedure.procedureId, actionMeta.option.value);
                break;
            case "remove-value":
            case "pop-value":
                console.log(actionMeta.removedValue.value);
                await removeUserFromPlanProcedure(parseInt(id), procedure.procedureId, actionMeta.removedValue.value);
                break;
            case "clear":
                await removeAllUsersFromPlanProcedure(parseInt(id), procedure.procedureId);
                break;
            default:
                break;
        }
        setSelectedUsers(e);
    };

    return (
        <div className="py-2">
            <div>
                {procedure.procedureTitle}
            </div>

            <ReactSelect
                className="mt-2"
                placeholder="Select User to Assign"
                isMulti={true}
                options={users}
                value={selectedUsers}
                onChange={handleAssignUserToProcedure}
            />
        </div>
    );
};

export default PlanProcedureItem;
