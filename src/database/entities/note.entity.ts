import { UserEntity } from './user.entity';
import { MapEntity } from './map.entity';
import {
    BelongsTo,
    Column,
    ForeignKey,
    Table,
    Model
} from "sequelize-typescript";
import {
    InferAttributes,
    InferCreationAttributes
} from "sequelize";

@Table({ modelName: "notes" })
export class NoteEntity extends Model<InferAttributes<NoteEntity>, InferCreationAttributes<NoteEntity>> {

    @Column
    text: string;

    @Column
    @ForeignKey(() => UserEntity)
    userId: number;

    @Column
    @ForeignKey(() => MapEntity)
    mapId: number;

    @Column
    public createdAt: Date = new Date();

    @Column
    public updatedAt: Date = new Date();

    @BelongsTo(() => UserEntity)
    user: UserEntity;

    @BelongsTo(() => MapEntity)
    map: MapEntity;
}