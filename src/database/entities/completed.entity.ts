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

@Table({ modelName: "completed" })
export class CompletedEntity extends Model<InferAttributes<CompletedEntity>, InferCreationAttributes<CompletedEntity>> {

    @Column
    @ForeignKey(() => MapEntity)
    mapId: number;

    @Column
    @ForeignKey(() => UserEntity)
    userId: number;

    @Column
    public createdAt: Date = new Date();

    @Column
    public updatedAt: Date = new Date();

    @Column
    public image: string;

    @BelongsTo(() => MapEntity)
    map: MapEntity;

    @BelongsTo(() => UserEntity)
    user: UserEntity;
}
